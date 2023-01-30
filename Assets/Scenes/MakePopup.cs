using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePopup : MonoBehaviour
{
    public GameObject popup;

    private void Start()
    {
        popup.SetActive(false);
    }

    public void Setpop()
    {
        popup.SetActive(true);
        Invoke("popup", 0.1f);
    }
}