using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class UIDataManager : MonoBehaviour
{
    [SerializeField] private Image hpGauge;
    private bool isActive;

    public void UpdateUI(float current, float max, bool isbool)
    {
        

        hpGauge.fillAmount = current / max;
        if (!isActive && !isbool) StartCoroutine(ActiveHP());
    }

    private IEnumerator ActiveHP()
    {
        isActive = true;
        hpGauge.transform.parent.gameObject.SetActive(isActive);
        yield return new WaitForSeconds(3f);
        isActive = false;
        hpGauge.transform.parent.gameObject.SetActive(isActive);
    }
}
