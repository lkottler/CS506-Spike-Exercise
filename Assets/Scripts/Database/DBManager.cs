using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms;

public class DBManager : MonoBehaviour
{
    public void RefreshDatabase()
    {
        StartCoroutine(RefreshUsers());
        //db.RefreshUsers();
    }
    private IEnumerator RefreshUsers()
    {
        //string uri = "http://pages.cs.wisc.edu/~lkottler/sqlconnect/populate.php";
        string uri = "http://localhost/sqlconnect/populateUsers.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                string[] rawLines = webRequest.downloadHandler.text.Split('\n');
                if (DB.users.Count > 0)
                    DB.users.Clear();   
                foreach (string rawDetails in rawLines)
                {
                    User tempUser = new User();
                    if (rawDetails.Length == 0) break;
                    string[] details = rawDetails.Split('\t');
                    tempUser.id = int.Parse(details[0]);
                    tempUser.username = details[1];
                    tempUser.contact = details[2];
                    tempUser.address = details[3];
                    DB.users.Add(tempUser);
                }
            }
        }
        Debug.Log("Counting Users: " + DB.users.Count);
    }
    public void LogOut()
    {
        DB.activeUser = null;
        DB.activeUsername = null;
    }
}
