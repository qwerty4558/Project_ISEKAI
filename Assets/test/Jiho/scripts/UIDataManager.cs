using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class UIDataManager : MonoBehaviour
{
    [SerializeField] private Image hpGauge;

    public void UpdateUI(float current, float max)
    {
        hpGauge.fillAmount = current / max;
    }

}
