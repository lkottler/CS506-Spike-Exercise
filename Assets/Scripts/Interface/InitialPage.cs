using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialPage : MonoBehaviour
{
    public Text usernameDisplay, contactDisplay, addressDisplay;

    private void Start()
    {
        if (DBManager.LoggedIn)
        {
            Debug.Log("Attempting to create texts: " + DBManager.contact);
            usernameDisplay.text = "User: " + DBManager.username;
            contactDisplay.text = DBManager.contact;
            addressDisplay.text = DBManager.address;
        }
    }
    public void Load_loginPage()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Attempted to Load Login Page");
    }

    public void Load_registerPage()
    {
        Debug.Log("Attempting to load RegisterPage");
        SceneManager.LoadScene(2);
    }
}
