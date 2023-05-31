using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultisceneDatapass : SingletonMonoBehaviour<MultisceneDatapass>
{
    [ReadOnly] public string PortalDestinationID = string.Empty;

    [ReadOnly] public List<Result_Item> craftableItems;

    protected override void Awake()
    {
        base.Awake();
        craftableItems = new List<Result_Item>();
    }
}
