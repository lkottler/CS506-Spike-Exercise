﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB
{
    public static DBManager manager;
    public static List<User> users = new List<User>();
    public static User activeUser;
    public static string activeUsername;
    public static bool LoggedIn { get { return activeUser != null; } }
    public static void UpdateActiveUser()
    {
        if (DB.activeUsername == null)
        {
            Debug.Log("There is no active user.");
            return;
        }
        if (DB.users.Count < 1)
        {
            Debug.Log("There are no users stored.");
            return;
        }
        foreach (User u in DB.users)
        {
            Debug.Log("comparing " + u.username + ":" + DB.activeUsername);
            if (u.username == DB.activeUsername)
            {
                DB.activeUser = u;
                break;
            }
        }
    }
}