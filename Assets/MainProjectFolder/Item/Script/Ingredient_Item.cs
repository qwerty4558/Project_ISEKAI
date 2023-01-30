using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient_Item
{
    public int id;
    public string name;
    public string name_KR;
    public string icon_File_Name;
    

    

    public Ingredient_Item(string _id, string _name, string _name_KR, string _icon_File_Name)
    {
        id = Convert.ToInt32(_id);
        name = _name;
        name_KR = _name_KR;
        icon_File_Name = _icon_File_Name;
    }

}


[System.Serializable]
public class Result_Item
{
    public int main_Ingredient_TID;
    public int main_Count;
    public int sub_Ingredient_TID;
    public int sub_Count;
    public int result_ID;
    public string result_Item_Name;
    public int result_Item_Price;
    public string icon_File_Name;


    public Result_Item(
        string _main_Ingredient_TID, 
        string _main_Count, 
        string _sub_Ingredient_TID, 
        string _sub_Count, 
        string _result_ID, 
        string _result_Item_Name,
        string _result_Item_Price,
        string _icon_File_Name)
    {
        main_Ingredient_TID = Convert.ToInt32(_main_Ingredient_TID);
        main_Count = Convert.ToInt32(_main_Count);
        sub_Ingredient_TID = Convert.ToInt32(_sub_Ingredient_TID);
        sub_Count = Convert.ToInt32(_sub_Count);
        result_ID = Convert.ToInt32(_result_ID);
        result_Item_Name = _result_Item_Name;
        result_Item_Price = Convert.ToInt32(_result_Item_Price);
        icon_File_Name = _icon_File_Name;
    }
}
