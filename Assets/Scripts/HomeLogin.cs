using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeLogin : MonoBehaviour
{
    public void Load_initialPage()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Attempted to Load Initial Page");
    }
}