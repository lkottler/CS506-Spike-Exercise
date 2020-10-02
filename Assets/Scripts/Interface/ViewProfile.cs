using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViewProfile : MonoBehaviour
{

    private Text usernameDisplay1, usernameDisplay2;
    private Image pfp; public InputField nameField;
    private InputField contactField, addressField, equipmentField;

    private GameObject canvas;
    private GameObject hives;
    private GameObject hiveView;
    private Button createHive;

    private int x = -314, y = 920;


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
        for (int i = 0; i < thisUser.hives.Count; i++)
        {   
            Hive hive = thisUser.hives[i];
            var btn = (GameObject)Instantiate(Resources.Load("hiveButton", typeof(GameObject))) as GameObject;
            if (btn == null) continue;
            // place button on profiles object
            btn.transform.SetParent(hives.transform);
            // set the text
            Text text1 = btn.transform.GetChild(0).GetComponent<Text>();
            Text text2 = btn.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            text1.text = hive.name;
            text2.text = hive.name;

            // place the button on x and y
            btn.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(x + +158 * (i % 5), y + (i / 5) * -166, 0);
            btn.GetComponent<Button>().onClick.AddListener(delegate { btn_loadHive(hive); });
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
            = new Vector3(x + +158 * (DB.viewedUser.hives.Count % 5), y + (DB.viewedUser.hives.Count / 5) * -166, 0);
        // Clicking this button will change it to act like a typical hive button, and call this function to recreate itself.
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
            createHive.GetComponent<Button>().onClick.AddListener(delegate
            {
                btn_loadHive(hive);
            });

            // Create a new button there, then open the hive panel
            createNewHiveBtn();
            btn_loadHive(hive);
        });
    }
    public void btn_loadHive(Hive hive)
    {
        bool interact = (DB.viewedUser == DB.activeUser);
        hiveView = (GameObject)Instantiate(Resources.Load("HiveView", typeof(GameObject))) as GameObject;
        if (hiveView == null) return;
        hiveView.transform.SetParent(canvas.transform);
        hiveView.GetComponent <RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
    }

    public Hive btn_newHive()
    {
        Hive hive = new Hive();
        DB.viewedUser.hives.Add(hive);
        return hive;
    }

    public void saveAndReturn()
    {
        //TODO save here
        DB.viewedUser.address = addressField.text;
        DB.viewedUser.contact = contactField.text;
        DB.viewedUser.equipment = equipmentField.text;

        SceneManager.LoadScene(DB.returnScene);
    }
}
