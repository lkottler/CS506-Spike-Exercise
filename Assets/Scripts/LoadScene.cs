using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private AssetBundle myScenes;
    private string[] scenePaths;

    // Start is called before the first frame update
    void Start()
    {
        myScenes = AssetBundle.LoadFromFile("Assets/Scenes");
        scenePaths = myScenes.GetAllScenePaths();
    }

    // This runs when the GUI is initialized
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Change Scene"))
        {
            UnityEngine.Debug.Log("Scene2 loading: " + scenePaths[1]);
            SceneManager.LoadScene(scenePaths[1], LoadSceneMode.Single);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
