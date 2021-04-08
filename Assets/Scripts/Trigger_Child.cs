using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Child : MonoBehaviour
{
    [SerializeField] private GameObject ChildPlane;

    private Cloth PlanCloth;
    private Transform PlanTransform;

    // Start is called before the first frame update
    void Start()
    {
        PlanCloth = ChildPlane.GetComponent<Cloth>();
        PlanTransform = ChildPlane.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PlanTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if(other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("AhBon");
            PlanCloth.useGravity = false;
        }*/

        Debug.Log("AhBon");
        PlanCloth.useGravity = false;
    }
}
