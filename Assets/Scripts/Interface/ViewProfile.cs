using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ViewProfile : MonoBehaviour
{
    private bool loading = false;

    private Text usernameDisplay1, usernameDisplay2;
    private Image pfp; public InputField nameField;
    private InputField contactField, addressField, equipmentField;

    private GameObject canvas;
    private GameObject hives;
    private GameObject hiveView;
    private Button createHive;

    private int x = -314, y = 920;
    private int privateHives = 0;


    public Button returnBtn;
    // Start is called before the first frame update
    void Start()
    {
        // Fetch all the gameobjects
        canvas = GameObject.Find("Canvas");
        usernameDisplay1 = GameObject.Find("Canvas/Username").GetComponent<Text>();
        usernameDisplay2 = GameObject.Find("Canvas/Username/FrontText").GetComponent<Text>();
        pfp = GameObject.Find("Canvas/pfp").GetComponent<Image>();
        string text = DB.viewedUser.username + "'s Profile";
        usernameDisplay1.text = text;
        usernameDisplay2.text = text;
        pfp.sprite = DB.viewedUser.pfp;

        contactField = GameObject.Find("Canvas/ContactInput").GetComponent<InputField>();
        addressField = GameObject.Find("Canvas/AddressInput").GetComponent<InputField>();
        equipmentField = GameObject.Find("Canvas/EquipmentInput").GetComponent<InputField>();
        contactField.text = DB.viewedUser.contact;
        addressField.text = DB.viewedUser.address;
        equipmentField.text = DB.viewedUser.equipment;

        if (DB.viewedUser == DB.activeUser)
        {
            contactField.interactable = true;
            addressField.interactable = true;
            equipmentField.interactable = true;
        }

        hives = GameObject.Find("Canvas/HivesBG/Hives");
        createHives();
    }
    private void createHives()
    {
        User thisUser = DB.viewedUser;
        privateHives = 0;
        for (int i = 0; i < thisUser.hives.Count; i++)
        {   
            Hive hive = thisUser.hives[i];
            if (DB.activeUser == DB.viewedUser || hive.isPublic)
            {
                var btn = (GameObject)Instantiate(Resources.Load("hiveButton", typeof(GameObject))) as GameObject;
                if (btn == null) continue;
                // place button on profiles object
                btn.transform.SetParent(hives.transform);
                // set the text
                Text text1 = btn.transform.GetChild(0).GetComponent<Text>();
                Text text2 = btn.transform.GetChild(0).GetChild(0).GetComponent<Text>();
                text1.text = (hive.name == "") ? "Hive" : hive.name;
                text2.text = (hive.name == "") ? "Hive" : hive.name;

                // place the button on x and y
                btn.GetComponent<RectTransform>().anchoredPosition3D = 
                    new Vector3(x + +158 * ((i - privateHives) % 5), y + ((i - privateHives) / 5) * -166, 0);
                btn.GetComponent<Button>().onClick.AddListener(delegate { 
                    btn_loadHive(hive);
                    text1.text = (hive.name == "") ? "Hive" : hive.name;
                    text2.text = (hive.name == "") ? "Hive" : hive.name;
                });
            }
            else
            {
                privateHives++;
            }
        }
        if (DB.viewedUser == DB.activeUser)
            createNewHiveBtn();

    }
    private void createNewHiveBtn()
    {
        var newHiveBtn = (GameObject)Instantiate(Resources.Load("newHiveButton", typeof(GameObject))) as GameObject;
        if (newHiveBtn == null)
            return;
        createHive = newHiveBtn.GetComponent<Button>();
        newHiveBtn.transform.SetParent(hives.transform);
        newHiveBtn.GetComponent<RectTransform>().anchoredPosition3D
            = new Vector3(x + +158 * ((DB.viewedUser.hives.Count - privateHives) % 5), y + ((DB.viewedUser.hives.Count - privateHives) / 5) * -166, 0);
        // Clicking this button will change it to act like a typical hive button, and call this function to recreate itself.
        Text text1 = newHiveBtn.transform.GetChild(0).GetComponent<Text>();
        Text text2 = newHiveBtn.transform.GetChild(0).GetChild(0).GetComponent<Text>();

        newHiveBtn.GetComponent<Button>().onClick.AddListener(delegate 
        { 
            Hive hive = btn_newHive();
            createHive.transform.GetChild(0).GetComponent<Text>().text = hive.name;
            createHive.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = hive.name;

            // Change the alpha back to max
            ColorBlock cb = createHive.colors;
            Color newColor = cb.normalColor;
            newColor.a = 1f;
            cb.normalColor = newColor;
            createHive.colors = cb;

            // Change the OnClick
            createHive.GetComponent<Button>().onClick.RemoveAllListeners();
            createHive.GetComponent<Button>().onClick.AddListener(delegate{ 
                btn_loadHive(hive);
                text1.text = (hive.name == "") ? "Hive" : hive.name;
                text2.text = (hive.name == "") ? "Hive" : hive.name;
            });

            // Create a new button there, then open the hive panel
            createNewHiveBtn();
            btn_loadHive(hive);
        });
    }
    public string btn_loadHive(Hive hive)
    {
        bool interact = (DB.viewedUser == DB.activeUser);
        hiveView = (GameObject)Instantiate(Resources.Load("HiveView", typeof(GameObject))) as GameObject;
        if (hiveView == null) return hive.name; 
        hiveView.transform.SetParent(canvas.transform);
        hiveView.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
        InputField[] fields = hiveView.GetComponentsInChildren<InputField>();

        if (interact)
        {
            foreach (InputField f in fields)
            {
                f.interactable = interact;
            }
            hiveView.GetComponentInChildren<Toggle>().interactable = interact;
            hiveView.GetComponentsInChildren<Button>()[1].interactable = interact;
        }

        fields[0].text = (hive.name == "Hive") ? "" : hive.name;
        fields[1].text = hive.health.ToString();
        fields[2].text = hive.honey.ToString();
        fields[3].text = hive.queenProduction.ToString();
        fields[4].text = hive.equipment;
        fields[5].text = hive.profit.ToString();

        hiveView.GetComponentInChildren<Toggle>().isOn = hive.isPublic;

        // Save & Return button
        
        hiveView.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate 
        {
            hive.name = fields[0].text ;
            hive.health = int.Parse(fields[1].text);
            hive.honey = int.Parse(fields[2].text);
            hive.queenProduction = int.Parse(fields[3].text);
            hive.equipment = fields[4].text;
            hive.profit = int.Parse(fields[5].text);

            hive.isPublic = hiveView.GetComponentInChildren<Toggle>().isOn;

            loading = true;
            StartCoroutine(PublishHive(hive));
            StartCoroutine(RefreshScene());
        });

        // Delete Hive Button
        hiveView.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate
        {
            DB.viewedUser.hives.Remove(hive);
            loading = true;
            StartCoroutine(DeleteHive(hive));
            StartCoroutine(RefreshScene());
        });

        return hive.name;
    }

    IEnumerator PublishHive(Hive hive)
    {
        WWWForm form = new WWWForm();
        form.AddField("ownerID", hive.ownerID);
        form.AddField("id", hive.id);
        form.AddField("isPublic", (hive.isPublic) ? "1" : "0");
        form.AddField("name", hive.name);
        form.AddField("health", hive.health);
        form.AddField("honeyStore", hive.honey);
        form.AddField("queenProduction", hive.queenProduction);
        form.AddField("equipment", hive.equipment);
        form.AddField("profit", hive.profit);

        string url = "http://pages.cs.wisc.edu/~lkottler/sqlconnect/updateHive.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                if (hive.id == -1)
                    hive.id = int.Parse(webRequest.downloadHandler.text);
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
        loading = false;
    }

    IEnumerator DeleteHive(Hive hive)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", hive.id);
        string url = "http://pages.cs.wisc.edu/~lkottler/sqlconnect/deleteHive.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
        }
        loading = false;
    }
    IEnumerator UpdateUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DB.viewedUser.username);
        form.AddField("address", addressField.text);
        form.AddField("contact", contactField.text);
        form.AddField("equipment", equipmentField.text);

        string url = "http://pages.cs.wisc.edu/~lkottler/sqlconnect/updateUser.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                DB.viewedUser.address = addressField.text;
                DB.viewedUser.contact = contactField.text;
                DB.viewedUser.equipment = equipmentField.text;
            }
        }
        loading = false;
    }


    IEnumerator RefreshScene()
    {
        while (loading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene("ViewProfile");
    }

    IEnumerator ReturnScene()
    {
        while (loading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene(DB.returnScene);
    }

    public Hive btn_newHive()
    {
        Hive hive = new Hive();
        hive.ownerID = DB.viewedUser.id;
        DB.viewedUser.hives.Add(hive);
        return hive;
    }

    public void saveAndReturn()
    {
        loading = true;
        StartCoroutine(UpdateUser());
        StartCoroutine(ReturnScene());
    }
}
