using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum PUZZLE_PIECE
{
    NO_PIECE = 0,
    PIECE = 1,
}

public class Ingredient_Item : SerializedScriptableObject
{ 
    public int id;
    public Sprite itemImage;
    public string status;
    public string route;
    public int count;
    public bool isAlchemy;
    public new string name;
    public string name_KR;
    public string icon_File_Name;

    public PUZZLE_PIECE[,] puzzle;


    public Ingredient_Item(Sprite _image, string _name, string _status, string _route, bool _isAlchemy, int _count)
    {
        //this.id = Convert.ToInt32(_id);
        this.itemImage = _image;
        this.name = _name;
        //this.name_KR = _name_KR;
        //this.icon_File_Name = _icon_File_Name;
        this.status = _status;
        this.count = _count;
        this.route = _route;
        this.isAlchemy = _isAlchemy;
    }
}

