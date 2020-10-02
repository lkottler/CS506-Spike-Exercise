using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive
{
    public int id;
    public int ownerID;
    public string name;
    public bool isPublic;
    public int health, honey, queenProduction, profit;
    public string equipment;
    public Hive()
    {
        this.ownerID = -1;
        this.name = "Hive";
        this.isPublic = true;
        this.health = 0;
        this.honey = 0;
        this.queenProduction = 0;
        this.profit = 0;
        this.equipment = "";
    }
}
