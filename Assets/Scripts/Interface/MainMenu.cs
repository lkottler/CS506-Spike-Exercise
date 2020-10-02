using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.InteropServices.ComTypes;
using System.Net;
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;

public class MainMenu : MonoBehaviour
{
    public Text usernameDisplay1, usernameDisplay2;

    private List<GameObject> buttons;
    private GameObject canvas;
    private GameObject profiles;
    private GameObject btn1, btn2;

    private void Start()
    {
        // DB.users.Add(new User());
        profiles = GameObject.Find("Canvas/ProfileBG/Profiles");
        canvas = GameObject.Find("Canvas");
        usernameDisplay1 = GameObject.Find("Canvas/Username").GetComponent<Text>();
        usernameDisplay2 = GameObject.Find("Canvas/Username/FrontText").GetComponent<Text>();

        // Instantiate top right buttons
        btn1 = (GameObject)Instantiate(Resources.Load("transitionButton", typeof(GameObject))) as GameObject;
        btn2 = (GameObject)Instantiate(Resources.Load("transitionButton", typeof(GameObject))) as GameObject;
        btn1.transform.SetParent(canvas.transform);
        btn2.transform.SetParent(canvas.transform);
        btn1.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(230, 250, 0);
        btn2.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(412, 250, 0);

        if (DB.LoggedIn)
        {
            string text = "Welcome " + DB.activeUser.username + "!";
            usernameDisplay1.text = text;
            usernameDisplay2.text = text;
            loggedUserButtons();
        } 
        else
        {
            noUserButtons();
        }
        createProfiles();
    }

    public void btn_logout()
    {
        string text = "No user logged in.";
        usernameDisplay1.text = text;
        usernameDisplay2.text = text;
        DB.logOut();
        noUserButtons();
    }

    public void btn_loadUser(User u)
    {
        DB.viewedUser = u;
        //SceneManager.LoadScene("UserPage");
    }

    // This function will iterate through every user in the Database, creating an associated button.
    private void createProfiles()
    {
        int x = -410, y = 920;
        for (int i = 0; i < DB.users.Count; i++)
        {
            var btn = (GameObject)Instantiate(Resources.Load("profileButton", typeof(GameObject))) as GameObject;
            if (btn == null) continue;
            // place button on profiles object
            btn.transform.SetParent(profiles.transform, false);

            // set the text
            Text text1 = btn.transform.GetChild(0).GetComponent<Text>();
            Text text2 = btn.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            text1.text = DB.users[i].username;
            text2.text = DB.users[i].username;
            if (DB.users[i] == DB.activeUser)
                text2.color = Color.red;

            // place the button on x and y
            btn.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(x + +164 * (i % 6), y + (i / 6) * -166, 0);
            btn2.GetComponentInChildren<Button>().onClick.AddListener(delegate { btn_loadUser(DB.users[i]); });
        }
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
        // Change the first button to load the login page
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
