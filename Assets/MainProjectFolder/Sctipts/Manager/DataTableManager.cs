using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DataTableManager
{
    static private DataTableManager _instance = null;
    static public DataTableManager instance => _instance;

    public DataTable_NpcShopDialog npcShopDialog = null;   

    static public DataTableManager InitTable(TextAsset[] templates)
    {
        DataTableManager newInstance = null;
        foreach (TextAsset t in templates)
        {
            //  * 별도의 mapper 를 제공하는게 좋지만, 룰-베이스도 괜찮다. 룰만 지켜진다면..
            string ruledName = "CSV" + t.name;

            switch (ruledName)
            {
                case nameof(CSVNpcDialogTemplate):
                    newInstance.npcShopDialog = new DataTable_NpcShopDialog(t.text);
                    break;
            }
        }

        _instance = newInstance;
        return _instance;
    }

    static public void ForceDestroy()
    {
        _instance = null;
    }
}

public class DataTable_NpcShopDialog
{
    public DataTable_NpcShopDialog(string text)
    {

    }
}