using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class Registration : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public InputField contactField;
    public InputField addressField;

    public Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {

        /* Depricated
        WWW www = new WWW(url, form);
        Debug.Log("Attempting to reach: " + url);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("User created successfully.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("User creation failed. Error #" + www.text);
        }
        */

        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        form.AddField("contact", contactField.text);
        form.AddField("address", addressField.text);
        string url = "http://pages.cs.wisc.edu/~lkottler/sqlconnect/register.php";
        //string url = "http://localhost/sqlconnect/register.php";

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.downloadHandler.text[0] == '0')
            {
                Debug.Log("User created successfully.");
                UnityEngine.SceneManagement.SceneManager.LoadScene(3);
            }
            else
            {
                Debug.Log("User creation failed. Error #" + webRequest.downloadHandler.text);
            }
        }
    }

    public void returnScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(DB.returnScene);
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && 
                                     passwordField.text.Length >= 8 &&
                                     contactField.text.Length >= 1 &&
                                     addressField.text.Length >= 1);
    }
}