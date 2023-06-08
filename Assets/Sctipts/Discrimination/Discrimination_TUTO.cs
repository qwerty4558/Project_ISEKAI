using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discrimination_TUTO : MonoBehaviour
{
    private void OnDisable()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
            player.ControlEnabled = true;
        CursorManage.instance.HideMouse();
    }

    private void Update()
    {
        if (gameObject.activeSelf) CursorManage.instance.ShowMouse();
    }
}
