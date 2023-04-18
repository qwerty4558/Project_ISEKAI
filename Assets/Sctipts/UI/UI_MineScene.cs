using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MineScene : UI_View
{
    [SerializeField] private Button _startBTN;
    [SerializeField] private Button _endBTN;

    public override void Inittialize()
    {
        //_startBTN.onClick.AddListener(() => UIManager.Show);
    }
}
