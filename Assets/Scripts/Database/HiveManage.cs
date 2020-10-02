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
    private int errorCounter = 0;

    public Button submitButton;

    public void CallSubmitHiveInfo()
    {
        StartCoroutine(SubmitHiveInfo());
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
    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8);
    }
}
