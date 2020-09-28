using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceLogic : MonoBehaviour
{
    public GameObject FrontPage;
    public GameObject LoginPage;
    public GameObject RegisterPage;

    // Start is called before the first frame update
    void Start()
    {
        FrontPage = GameObject.Find("canvas_FrontPage");
        LoginPage = GameObject.Find("canvas_LoginPage");
        RegisterPage = GameObject.Find("canvas_RegisterPage");

        FrontPage.SetActive(true);
        LoginPage.SetActive(false);
        RegisterPage.SetActive(false);
        // Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btn_loginScript()
    {
        FrontPage.SetActive(false);
        LoginPage.SetActive(true);
    }

    public void btn_registerScript()
    {
        FrontPage.SetActive(false);
        RegisterPage.SetActive(true);
    }

    public void btn_confirmScript()
    {
        LoginPage.SetActive(false);
        //check database for valid entry

    }
}
