using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{


    public GameObject leCloth;
    public GameObject leCollider;


    public CapsuleCollider[] test1;








    void Start()
    {




        leCloth.GetComponent<Cloth>().capsuleColliders = test1; 
    }

   
}
