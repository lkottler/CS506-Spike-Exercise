using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public bool loadingDB = false;
    // Start is called before the first frame update
    void Start()
    {
        DB.manager = gameObject.AddComponent(typeof(DBManager)) as DBManager;
        DB.hiveManager = gameObject.AddComponent(typeof(HiveManage)) as HiveManage;
        StartCoroutine(init());
        StartCoroutine(ReturnScene());
    }

    IEnumerator init()
    {
        loadingDB = true;
        yield return DB.manager.RefreshDatabase();
        loadingDB = false;
    }
    IEnumerator ReturnScene()
    {
        while (loadingDB)
        {
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene(DB.returnScene);
    }
}
