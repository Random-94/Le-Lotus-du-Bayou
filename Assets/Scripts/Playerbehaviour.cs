using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Playerbehaviour : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera CinemachineCam;
    [SerializeField] private Camera Camera;

    [SerializeField] private float PlayerTurnSpeed;
    [SerializeField] private float PlayerSpeed;
    [SerializeField] private float PlayerJumpForce;

    [SerializeField] private float Gravity;

    [SerializeField] private float CurrentFuelJetPack;
    [SerializeField] private float JetPackForce;
    [SerializeField] private float LoadingJetPackSpeed;

    [SerializeField, Range(0,100)] private float FuelJetPackMax;

    [SerializeField] private Transform PlayerFeet;

    [SerializeField] LayerMask GroundMask;

    private Controls controls;
    private CharacterController CharaController;

    private Vector2 direction;

    private bool isJumping;
    private bool isThrowing;
    private bool JetPackOn;
    private bool CanUseJetPack;

    private Vector3 PlayerMove;
    private Vector3 DirectionToMove;
    private Vector3 PlayerOrientation;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;

        controls.Player.Jump.performed += OnJumpPerformed;
        controls.Player.Jump.canceled += OnJumpCanceled;

        controls.Player.JetPack.performed += OnJetPackPerformed;
        controls.Player.JetPack.canceled += OnJetPackCanceled;

        controls.Player.Throw.performed += OnThrowPerformed;
        controls.Player.Throw.canceled += OnThrowCanceled;

        CharaController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentFuelJetPack = FuelJetPackMax;
    }

    // Update is called once per frame
    void Update()
    {
        DirectionToMove = ApplyMove() + ApplyJumpJetPack() + ApplyGravity();

        CharaController.Move(DirectionToMove * Time.deltaTime);

        if(JetPackOn)
        {
            CanUseJetPack = true;
            CurrentFuelJetPack -= 1;
            Debug.Log(CurrentFuelJetPack);

            if(CurrentFuelJetPack <= 0)
            {
                JetPackOn = false;
            }
        }

        ReFuelJetPack();
    }

    private Vector3 ApplyMove()
    {
        if(PlayerMove == Vector3.zero)
        {
            var rotation2 = Quaternion.LookRotation(PlayerMove);
            rotation2 *= Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0); 
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation2, PlayerTurnSpeed * Time.deltaTime);

            PlayerOrientation = rotation2 * Vector3.zero;

            return PlayerOrientation.normalized;
        }

        var rotation = Quaternion.LookRotation(PlayerMove);
        rotation *= Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, PlayerTurnSpeed * Time.deltaTime);

        PlayerOrientation = rotation * Vector3.forward;

        return PlayerOrientation.normalized * PlayerSpeed;
    }

    private Vector3 ApplyJumpJetPack()
    {
        if (isJumping && DirectionToMove.y == 0)
        {
            var heightSpeed = Mathf.Sqrt(PlayerJumpForce * -2 * Gravity);
            var JumpVector = new Vector3(0, heightSpeed, 0); 
            return JumpVector;
        }
        else if (isJumping == false && JetPackOn && CurrentFuelJetPack > 0)
        {
            var JetPackAscending = Mathf.Sqrt(JetPackForce);
            var JetPackVector = new Vector3(0, JetPackAscending, 0);
            Debug.Log("Yahou");

            if (CurrentFuelJetPack == 0)
            {
                JetPackOn = false;
            }

            return JetPackVector;
        }
        return Vector3.zero;
    }

    private Vector3 ApplyGravity()
    {
        var startRaycastPos = PlayerFeet.position;
        var Groundraycast = Physics.Raycast(startRaycastPos, Vector3.down, 0.1f, GroundMask);

        var DirectionToFall = Vector3.zero;

        if (Groundraycast)
        {
            DirectionToMove.y = 0; 
        }
        else
        {
            DirectionToFall = new Vector3(0, DirectionToMove.y + Gravity * Time.deltaTime, 0);
        }

        return DirectionToFall;
    }

    private void ReFuelJetPack()
    {
        if(JetPackOn == false && CurrentFuelJetPack < FuelJetPackMax)
        {
            CurrentFuelJetPack += LoadingJetPackSpeed;
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<Vector2>();
        Debug.Log(direction);

        PlayerMove = new Vector3(direction.x, 0, direction.y);
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero;

        PlayerMove = Vector3.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        isJumping = true;
        Debug.Log("Jump OK");
    }

    private void OnJumpCanceled(InputAction.CallbackContext obj)
    {
        isJumping = false;
    }

    private void OnJetPackPerformed(InputAction.CallbackContext obj)
    {
        JetPackOn = true;
        
    }

    private void OnJetPackCanceled(InputAction.CallbackContext obj)
    {
        JetPackOn = false;
    }

    private void OnThrowPerformed(InputAction.CallbackContext obj)
    {
        isThrowing = true;
        Debug.Log("Throw OK");
    }

    private void OnThrowCanceled(InputAction.CallbackContext obj)
    {
        isThrowing = false;
    }
}
