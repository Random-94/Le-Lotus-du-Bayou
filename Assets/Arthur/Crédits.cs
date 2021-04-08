using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crédits : MonoBehaviour
{
    public void change()
    {
        SceneManager.LoadScene("Credits");
        Debug.Log("crédit");
    }
}