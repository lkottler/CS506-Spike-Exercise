using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceLogic : MonoBehaviour
{
    //public GameObject FrontPage;
    //public GameObject LoginPage;
    //public GameObject RegisterPage;
    //public GameObject ManagePage;

    // Start is called before the first frame update
    void Start()
    {
        //FrontPage = GameObject.Find("canvas_FrontPage");
        //LoginPage = GameObject.Find("canvas_LoginPage");
        //RegisterPage = GameObject.Find("canvas_RegisterPage");
        //ManagePage = GameObject.Find("canvas_ManagePage");

        //SceneManager.LoadScene("InitialPage");
        //FrontPage.SetActive(true);
        //LoginPage.SetActive(false);
        //RegisterPage.SetActive(false);
        //ManagePage.SetActive(false);
        // Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btn_loginScript()
    {
        //FrontPage.SetActive(false);
        //LoginPage.SetActive(true);
        SceneManager.LoadScene("LoginPage");
    }

    public void btn_registerScript()
    {
        //FrontPage.SetActive(false);
        //RegisterPage.SetActive(true);
        SceneManager.LoadScene("RegisterPage");
    }

    public void btn_confirm_loginScript()
    {
       
        //LoginPage.SetActive(false);
        //check database for valid entry, load data

        //ManagePage.SetActive(true);
        SceneManager.LoadScene("ManagePage");
    }

    public void btn_confirm_registerScript()
    {
        //LoginPage.SetActive(false);
        //check database for valid entry, add entry to database

        //ManagePage.SetActive(true);
        SceneManager.LoadScene("ManagePage");

    }
}
