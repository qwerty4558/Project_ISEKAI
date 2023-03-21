using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ingredient_Item : ScriptableObject 
{ 
    public int id;
    public new string name;
    public string name_KR;
    public string icon_File_Name;
    

    

    public Ingredient_Item(string _id, string _name, string _name_KR, string _icon_File_Name)
    {
        this.id = Convert.ToInt32(_id);
        this.name = _name;
        this.name_KR = _name_KR;
        this.icon_File_Name = _icon_File_Name;
    }
}

