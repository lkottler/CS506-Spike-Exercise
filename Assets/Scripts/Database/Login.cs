﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Login : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public Text errorDisplay;
    private int errorCounter = 0;

    public Button submitButton;

    public void CallLogin()
    {
        StartCoroutine(LoginUser());
    }

    IEnumerator LoginUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        string url = "http://pages.cs.wisc.edu/~lkottler/sqlconnect/login.php";
        //string url = "http://localhost/sqlconnect/login.php";

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.downloadHandler.text[0] == '0')
            {
                DB.activeUsername = nameField.text;
                DB.UpdateActiveUser();
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
            else
            {
                Debug.Log("Failed to login. Error #" + webRequest.downloadHandler.text);
                errorDisplay.text = "Failed to login! Attempt #" + ++errorCounter;
            }
        }

    }

    public void returnScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(DB.returnScene);
    }
    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }

}