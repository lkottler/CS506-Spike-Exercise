using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public int id;
    public string username;
    public string contact, address, equipment;
    public Sprite pfp;
    public List<Hive> hives = new List<Hive>();

    public User()
    {
        id = -1;
        username = "ERROR";
        contact = "NO CONTACT";
        address = "NO ADDRESS";
        equipment = "Shovel, Beekeeper's Suit";
        pfp = Resources.Load<Sprite>("bee");
    }
}
