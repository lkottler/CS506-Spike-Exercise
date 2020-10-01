using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DB.manager = gameObject.AddComponent(typeof(DBManager)) as DBManager;
        DB.manager.RefreshDatabase();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
