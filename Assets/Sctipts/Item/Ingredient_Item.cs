using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum PUZZLE_PIECE
{
    NO_PIECE = 0,
    PIECE = 1,
    END = 2
}

public class Ingredient_Item : SerializedScriptableObject
{ 
    public int id;
    public new string name;
    public string name_KR;
    public string icon_File_Name;

    public PUZZLE_PIECE[,] puzzle;
    

    public Ingredient_Item(string _id, string _name, string _name_KR, string _icon_File_Name)
    {
        this.id = Convert.ToInt32(_id);
        this.name = _name;
        this.name_KR = _name_KR;
        this.icon_File_Name = _icon_File_Name;
    }
}

