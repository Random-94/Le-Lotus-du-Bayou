using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw_Paper : MonoBehaviour
{
    [SerializeField] private float ThrowForce;
    //[SerializeField] private GameObject PrefabPaperTest;

    private bool IsThrowing;

    private Rigidbody myRB;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
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

        /*if(IsThrowing)
        {
            myRB.AddForce(PlayerHandTransform.forward * ThrowForce);
        }*/
    }
}
