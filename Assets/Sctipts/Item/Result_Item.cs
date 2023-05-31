using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Result_Item : SerializedScriptableObject
{
    public int grup_ID;
    public int main_Ingredient_TID;
    public int main_Count;
    public int sub_Ingredient_TID;
    public int sub_Count;
    public int result_ID;
    public int index;
    public int[,] road = new int[7,7];
    public string result_Item_Name;
    public string icon_File_Name;
    public Ingredient_Item outputItem;
    public bool ReCraftable;
    
    public PUZZLE_STATE[,] board;
    public int puzzle_usage;

    public Result_Item(
        string _grup_ID,
        string _main_Ingredient_TID,
        string _main_Count,
        string _sub_Ingredient_TID,
        string _sub_Count,
        string _result_ID,
        string _result_Item_Name,
        string _result_Item_Price,

        string _icon_File_Name)
    {
        grup_ID = Convert.ToInt32(_grup_ID);
        main_Ingredient_TID = Convert.ToInt32(_main_Ingredient_TID);
        main_Count = Convert.ToInt32(_main_Count);
        sub_Ingredient_TID = Convert.ToInt32(_sub_Ingredient_TID);
        sub_Count = Convert.ToInt32(_sub_Count);
        result_ID = Convert.ToInt32(_result_ID);
        result_Item_Name = _result_Item_Name;
        icon_File_Name = _icon_File_Name;
    }
}
public enum PUZZLE_STATE
{
    NoInsert = 0,
    Start = 1,
    Insert = 2,
    Finish = 3,
}