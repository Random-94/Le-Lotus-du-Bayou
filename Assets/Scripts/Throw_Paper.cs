using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw_Paper : MonoBehaviour
{
    [SerializeField] private float ThrowForce;
    [SerializeField] private GameObject ChildPlane;
    [SerializeField] private GameObject Player;

    [SerializeField] private GameObject leCloth;
    [SerializeField] private GameObject Collider1;
    [SerializeField] private GameObject Collider2;
    [SerializeField] private GameObject Collider3;
    [SerializeField] private GameObject Collider4;
    [SerializeField] private GameObject Collider5;
    [SerializeField] private GameObject Collider6;
    [SerializeField] private GameObject Collider7;
    [SerializeField] private GameObject Collider8;
    [SerializeField] private GameObject Collider9;
    [SerializeField] private GameObject Collider10;

    [SerializeField] private CapsuleCollider[] CapsuleColliderArray;

    private bool IsThrowing;
    private float distanceWithPlayer;

    private Rigidbody myRB;
    private Transform PlanTransform;
    private Transform PlayerTransform;
    private Cloth PlanCloth;

    // Start is called before the first frame update
    void Start()
    {

        myRB = GetComponent<Rigidbody>();
        PlanTransform = ChildPlane.GetComponent<Transform>();
        PlanCloth = ChildPlane.GetComponent<Cloth>();

        PlayerTransform = Player.GetComponent<Transform>();

        leCloth.GetComponent<Cloth>().capsuleColliders = CapsuleColliderArray;
    }

    // Update is called once per frame
    void Update()
    {

        var Player = GameObject.FindWithTag("Player");
        var PlayerScript = Player.GetComponent<Playerbehaviour>();

        var PlayerHand = GameObject.FindWithTag("PlayerHand");
        var PlayerHandTransform = PlayerHand.GetComponent<Transform>();

        var Camera = GameObject.FindWithTag("MainCamera");
        var CameraTransform = Camera.GetComponent<Transform>();

        IsThrowing = PlayerScript.isThrowing;

        myRB.AddForce(CameraTransform.forward * ThrowForce);

        DestroyGameObject();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("House"))
        {
            //Debug.Log("Bruuuuuh");
            //myRB.constraints = RigidbodyConstraints.FreezePosition;
            myRB.constraints = RigidbodyConstraints.FreezeAll;
            myRB.useGravity = false;

            myRB.AddForce(Vector3.zero);

            PlanCloth.useGravity = false;
        }
    }

    private void DestroyGameObject()
    {
        distanceWithPlayer = Vector3.Distance(PlayerTransform.position, transform.position);

        if (distanceWithPlayer > 100)
        {
            Destroy(gameObject);
        }
    }

}
