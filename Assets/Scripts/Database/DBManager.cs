using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    public IEnumerator RefreshDatabase()
    {
        yield return StartCoroutine(RefreshUsers());
        //db.RefreshUsers();
    }
    private IEnumerator RefreshUsers()
    {
        string uri = "http://pages.cs.wisc.edu/~lkottler/sqlconnect/populateUsers.php";
        //string uri = "http://localhost/sqlconnect/populateUsers.php";
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
                    /* TODO - encode image into sprite
                    if (details[4] != "")
                    {
                        var tex = new Texture2D(128, 128, TextureFormat.RGB24, false);

                        tex.ReadPixels(new Rect(0, 0, 128, 128), 0, 0);
                        tex.Apply();

                        byte[] bytes = tex.EncodeToPNG();
                        Destroy(tex);
                        tempUser.pfp = details[4].EncodeToPNG();

                    }
                    */
                    DB.users.Add(tempUser);
                }
            }
            Debug.Log("finished adding: " + DB.users.Count);
        }
    }

    public void LogOut()
    {
        DB.activeUser = null;
        DB.activeUsername = null;
    }
}
