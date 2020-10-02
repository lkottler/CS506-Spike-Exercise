using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.InteropServices.ComTypes;
using System.Net;
using System.Runtime.CompilerServices;

public class MainMenu : MonoBehaviour
{
    public Text usernameDisplay, contactDisplay, addressDisplay;

    private List<GameObject> buttons;
    private GameObject canvas;
    private GameObject profiles;
    private GameObject btn1, btn2;

    private void Start()
    {
        //DB.users.Add(new User());
        profiles = GameObject.Find("Canvas/ProfileBG/Profiles");
        canvas = GameObject.Find("Canvas");

        // Instantiate top right buttons
        btn1 = (GameObject)Instantiate(Resources.Load("transitionButton", typeof(GameObject))) as GameObject;
        btn2 = (GameObject)Instantiate(Resources.Load("transitionButton", typeof(GameObject))) as GameObject;
        btn1.transform.SetParent(canvas.transform);
        btn2.transform.SetParent(canvas.transform);
        btn1.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(230, 250, 0);
        btn2.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(412, 250, 0);


        if (DB.LoggedIn)
        {
            usernameDisplay.text = "Welcome " + DB.activeUser.username + "!";
            contactDisplay.text = "Contact: " + DB.activeUser.contact;
            addressDisplay.text = "Address: " + DB.activeUser.address;

            loggedUserButtons();

        } 
        else
        {
            noUserButtons();
        }

        int x = -420, y = 920;
        for (int i = 0; i < DB.users.Count; i++)
        {
            var btn = (GameObject)Instantiate(Resources.Load("profileButton", typeof(GameObject))) as GameObject;
            if (btn == null) continue;
            btn.transform.SetParent(profiles.transform, false);
            //btn.transform.localScale = Vector3.one;
            //btn.transform.localRotation = Quaternion.Euler(Vector3.zero);
            // Creating buttons every 152 pixels (152 - 128 = 24 pixel padding)
            btn.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(x+ + 152*(i%6), y + (i/6)*-152, 0);
        }

    }

    public void btn_logout()
    {
        DB.logOut();
        noUserButtons();
    }


    private void loggedUserButtons()
    {
        if (btn1 != null)
        {
            btn1.GetComponentInChildren<Text>().text = "My Profile";
        }
        if (btn2 != null)
        {
            btn2.GetComponentInChildren<Text>().text = "Logout";
            btn2.GetComponentInChildren<Button>().onClick.AddListener(delegate { btn_logout(); });
        }
    }
    private void noUserButtons()
    {
        //Change the first button to load the login page
        if (btn1 != null)
        {
            btn1.GetComponentInChildren<Text>().text = "Login";
            btn1.GetComponentInChildren<Button>().onClick.AddListener(delegate { Load_loginPage(); });
        }

        // Change the 2nd button to load the register page
        if (btn2 != null)
        {
            btn2.GetComponentInChildren<Text>().text = "Register";
            btn2.GetComponentInChildren<Button>().onClick.AddListener(delegate { Load_registerPage(); });

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
