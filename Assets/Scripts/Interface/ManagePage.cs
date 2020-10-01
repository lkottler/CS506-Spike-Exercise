using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagePage : MonoBehaviour
{

    public Text usernameDisplay, contactDisplay, addressDisplay;

    // Start is called before the first frame update
    void Start()
    {
        if (DBManager.LoggedIn)
        {
            Debug.Log("Attempting to create texts: " + DBManager.contact);
            usernameDisplay.text = "Welcome " + DBManager.username + "!";
            contactDisplay.text = "Contact: " + DBManager.contact;
            addressDisplay.text = "Address: " + DBManager.address;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public static class DBManager
{
    public static string username;
    public static string address, contact;


    public static bool LoggedIn { get { return username != null; } }

    public static void LogOut()
    {
        username = null;
    }
}

