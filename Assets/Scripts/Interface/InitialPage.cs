using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialPage : MonoBehaviour
{
    public void load_loginPage()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Attempted to Load Login Page");
    }

    public void load_registerPage()
    {
        Debug.Log("Attempting to load RegisterPage");
        SceneManager.LoadScene(2);
    }
}
