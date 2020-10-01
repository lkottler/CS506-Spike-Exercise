﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.InteropServices.ComTypes;

public class MainMenu : MonoBehaviour
{
    public Text usernameDisplay, contactDisplay, addressDisplay;

    private List<GameObject> buttons;
    private GameObject canvas;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");

        if (DB.LoggedIn)
        {
            usernameDisplay.text = "Welcome " + DB.activeUser.username + "!";
            contactDisplay.text = "Contact: " + DB.activeUser.contact;
            addressDisplay.text = "Address: " + DB.activeUser.address;
        }

        Debug.Log("User count: " + DB.users.Count);
        for (int i = 0; i < DB.users.Count; i++)
        {
            var btn = (GameObject)Instantiate(Resources.Load("profileButton", typeof(GameObject))) as GameObject;
            if (btn == null) continue;
            btn.transform.SetParent(canvas.transform);
            btn.transform.localScale = Vector3.one;
            btn.transform.localRotation = Quaternion.Euler(Vector3.zero);
            // Creating buttons every 152 pixels (152 - 128 = 24 pixel padding)
            btn.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-410 + 152*(i%6), 100 + (i/6)*-152, 0);
        }
    }



    public void Load_loginPage()
    {
        SceneManager.LoadScene("LoginPage");
        Debug.Log("Attempted to Load Login Page");
    }

    public void Load_registerPage()
    {
        Debug.Log("RegisterPage");
        SceneManager.LoadScene(2);
    }
}