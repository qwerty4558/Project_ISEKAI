using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Item Creator/Item")]
public class ItemCreator : ScriptableObject
{
    public int id;
    public string name;
    public string name_KR;
    public string icon_File_Name;

    public void Initialize(int id, string name, string name_KR, string icon_File_Name)
    {
        this.id = id;
        this.name = name;
        this.name_KR = name_KR;
        this.icon_File_Name = icon_File_Name;
    }
}
