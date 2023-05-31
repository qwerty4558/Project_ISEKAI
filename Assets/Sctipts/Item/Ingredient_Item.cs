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

public enum ItemType
{
    Equipment,
    Quest,
    Ingredient,
    Special_ingredient
}

[CreateAssetMenu(fileName = "New_Ingredient_Item", menuName = "ScriptableObjects/Equipment_Item", order = 1)]
public class Ingredient_Item : SerializedScriptableObject
{ 
    public int id;
    public Sprite itemImage;
    public string status;
    public string route;
    public int count;
    public int appraiseCount;
    public bool isAppraise;
    public new string name;
    public string name_KR;
    public string icon_File_Name;
    public Sprite itemPatternImage;

    public ItemType itemType;
    public PUZZLE_PIECE[,] puzzle;

    public Ingredient_Item(Sprite _image, string _name, string _status, string _route, int _count, int _appraiseCount)
    {
        this.itemImage = _image;
        this.name = _name;
        this.status = _status;
        this.count = _count;
        this.route = _route;
        this.appraiseCount = _appraiseCount;
    }
}

