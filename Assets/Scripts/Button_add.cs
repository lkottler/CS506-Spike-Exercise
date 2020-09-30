using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_add : MonoBehaviour
{
    public GameObject b;
    public Transform panel;

    public void CreateA()
    {
        GameObject a = (GameObject)Instantiate(b);
        a.transform.SetParent(panel.transform, false);
    }
}
