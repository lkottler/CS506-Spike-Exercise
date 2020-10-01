using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public int id;
    public string username;
    public string contact, address;

    public User()
    {
        id = -1;
        username = "ERROR";
        contact = "NO CONTACT";
        address = "NO ADDRSES";
    }
}
