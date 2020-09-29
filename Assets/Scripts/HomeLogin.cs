using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeLogin : MonoBehaviour
{
    public void changeHomeScene(string scenename) {
       Application.LoadLevel(scenename);
    }
}