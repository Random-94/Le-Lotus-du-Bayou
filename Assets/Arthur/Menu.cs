using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void goToMenu()
    {
        SceneManager.LoadScene("UI_Arthur");
        Debug.Log("menu");
    }
}