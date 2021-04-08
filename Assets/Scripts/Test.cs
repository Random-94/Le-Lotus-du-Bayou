using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject leCloth;
    [SerializeField] private GameObject leCollider;

    [SerializeField] private CapsuleCollider[] test1;

    void Start()
    {
        leCloth.GetComponent<Cloth>().capsuleColliders = test1; 
    }

   
}
