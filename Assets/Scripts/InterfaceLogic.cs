using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceLogic : MonoBehaviour
{
    public GameObject FrontPage;
    public GameObject LoginPage;

    // Start is called before the first frame update
    void Start()
    {
        FrontPage = GameObject.Find("canvas_FrontPage");
        LoginPage = GameObject.Find("canvas_LoginPage");

        FrontPage.SetActive(true);
        LoginPage.SetActive(false);
        Cursor.visible = true;
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

}
