using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class HiveManage : MonoBehaviour
{
    public InputField nameField;
    public InputField healthScoreField;
    public InputField honeyStoreField;
    public InputField queenProductionField;
    public InputField equipmentOnHiveField;
    public InputField profitField;
    public Text errorDisplay;

    public Button submitButton;

    public void CallSubmitHiveInfo()
    {
        StartCoroutine(SubmitHiveInfo());
    }

    public void CallGetHives()
    {
        StartCoroutine(GetHives());
    }

    IEnumerator SubmitHiveInfo() 
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        //form.AddField("healthScore", Int16.Parse(healthScoreField.text));
        form.AddField("healthScore", healthScoreField.text);
        // form.AddField("honeyStore", Int16.Parse(honeyStoreField.text));
        form.AddField("honeyStore", honeyStoreField.text);
        //form.AddField("queenProduction", Int16.Parse(queenProductionField.text));
        form.AddField("queenProduction", queenProductionField.text);
        // form.AddField("equipmentOnHive", equipmentOnHiveField.text);
        form.AddField("equipmentOnHive", equipmentOnHiveField.text);
        // form.AddField("profit", int.Parse(profitField.text, NumberStyles.AllowLeadingSign);
        form.AddField("profit", profitField.text);

        //TODO, I need to add PHP to connect with hive table?
        string url = "http://pages.cs.wisc.edu/~lkottler/sqlconnect/login.php";

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
        }
    }

    public IEnumerator GetHives()
    {
        WWWForm form = new WWWForm();
        form.AddField("ownerID", DB.viewedUser.id);

        string url = "http://localhost/sqlconnect/populateHives.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                string[] rawLines = webRequest.downloadHandler.text.Split('\n');
                if (DB.viewedUser.hives.Count > 0)
                    DB.viewedUser.hives.Clear();
                foreach (string rawDetails in rawLines)
                {
                    Hive tempHive = new Hive();
                    if (rawDetails.Length == 0) break;
                    string[] details = rawDetails.Split('\t');
                    tempHive.ownerID = DB.activeUser.id;
                    tempHive.isPublic = bool.Parse(details[0]);
                    tempHive.name = details[1];
                    tempHive.health = int.Parse(details[2]);
                    tempHive.honey = int.Parse(details[3]);
                    tempHive.queenProduction = int.Parse(details[4]);
                    tempHive.equipment = details[5];
                    tempHive.profit = int.Parse(details[6]);
                    tempHive.id = int.Parse(details[7]);
                    DB.viewedUser.hives.Add(tempHive);
                }
            }
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8);
    }
}
