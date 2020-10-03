using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private bool isLoading = false;

    public Text usernameDisplay1, usernameDisplay2;

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
        Debug.Log("attemping to load user: " + u.username);

        StartCoroutine(getHives());
        StartCoroutine(loadProfile());
    }
    IEnumerator loadProfile()
    {
        while (isLoading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene("ViewProfile");
    }
    IEnumerator getHives()
    {
        isLoading = true;
        yield return DB.hiveManager.GetHives();
        isLoading = false;
    }

    // This function will iterate through every user in the Database, creating an associated button.
    private void createProfiles()
    {
        int x = -410, y = 920;
        for (int i = 0; i < DB.users.Count; i++)
        {
            User user = DB.users[i];
            var btn = (GameObject)Instantiate(Resources.Load("profileButton", typeof(GameObject))) as GameObject;
            if (btn == null) continue;
            // place button on profiles object
            btn.transform.SetParent(profiles.transform, false);

            // set the text
            Text text1 = btn.transform.GetChild(0).GetComponent<Text>();
            Text text2 = btn.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            text1.text = user.username;
            text2.text = user.username;
            if (user == DB.activeUser)
                text2.color = Color.red;

            // change the user's sprite
            btn.GetComponent<Image>().sprite = user.pfp;

            // place the button on x and y
            btn.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(x + +164 * (i % 6), y + (i / 6) * -166, 0);
            btn.GetComponent<Button>().onClick.AddListener(delegate { btn_loadUser(user); });
        }
    }
    private void loggedUserButtons()
    {
        if (btn1 != null)
        {
            btn1.GetComponentInChildren<Text>().text = "My Profile";
            btn1.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            btn1.GetComponentInChildren<Button>().onClick.AddListener(delegate { btn_loadUser(DB.activeUser); });

        }
        if (btn2 != null)
        {
            btn2.GetComponentInChildren<Text>().text = "Logout";
            btn2.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            btn2.GetComponentInChildren<Button>().onClick.AddListener(delegate { btn_logout(); });
        }
    }
    private void noUserButtons()
    {
        // Change the first button to load the login page
        if (btn1 != null)
        {
            btn1.GetComponentInChildren<Text>().text = "Login";
            btn1.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            btn1.GetComponentInChildren<Button>().onClick.AddListener(delegate { Load_loginPage(); });
        }

        // Change the 2nd button to load the register page
        if (btn2 != null)
        {
            btn2.GetComponentInChildren<Text>().text = "Register";
            btn2.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
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
        SceneManager.LoadScene("RegisterPage");
    }
}
