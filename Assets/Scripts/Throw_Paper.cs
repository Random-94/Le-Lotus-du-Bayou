using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw_Paper : MonoBehaviour
{
    [SerializeField] private float ThrowForce;
    [SerializeField] private GameObject ChildPlane;
    [SerializeField] private GameObject EmptyEnd;

    [SerializeField] private GameObject[] GameObjectArray;
    [SerializeField] private CapsuleCollider[] CapsuleColliderArray;

    private bool IsThrowing;

    private Rigidbody myRB;
    private Transform PlanTransform;
    private Cloth PlanCloth;

    private CapsuleCollider capsule;
    public GameObject gjgjg;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        PlanTransform = ChildPlane.GetComponent<Transform>();
        PlanCloth = ChildPlane.GetComponent<Cloth>();

        GameObjectArray = GameObject.FindGameObjectsWithTag("Capsule");
        
        //CapsuleColliderArray = CapsuleCollider.FindObjectOfType(CapsuleCollider);

        //capsule = GameObject.FindWithTag("Capsule");

        for (int i = 0; i < GameObjectArray.Length; i++)
        {
            capsule = GetComponent<CapsuleCollider>();
            PlanCloth.capsuleColliders.SetValue(capsule, i);
 
        }

        
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


    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("House"))
        {
            Debug.Log("Bruuuuuh");
            myRB.constraints = RigidbodyConstraints.FreezePosition;
            myRB.constraints = RigidbodyConstraints.FreezeRotation;
            //PlanCloth.useGravity = false;
            //PlanTransform.rotation = Vector3.zero;
            myRB.AddForce(Vector3.zero);
        }
    }

}
