using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Quest : SerializedScriptableObject
{
    public string Quest_Name;
    public string NPC_File_Name;
    public string NPC_Name;
    public int Quest_Price;

    public Quest(
        string _Quest_Name,
        string _NPC_File_Name,
        string _NPC_Name,
        string _Quest_Price
	)

    {
        Quest_Name = _Quest_Name;
        NPC_File_Name = _NPC_File_Name;
        NPC_Name = _NPC_Name;
        Quest_Price = Convert.ToInt32(_Quest_Price);
    }
}