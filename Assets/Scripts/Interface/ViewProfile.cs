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
    public Button returnBtn;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
