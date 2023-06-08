using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManage : MonoBehaviour
{
    public static CursorManage instance;
    [SerializeField] Texture2D clickCursor;
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] SoundModule sound;


    private void Awake()
    {
        if(instance == null) instance = this;
    }
    private void Start()
    {
        Cursor.SetCursor(defaultCursor, new Vector2(0, 0), CursorMode.Auto);
    }

    public void OnMouseOver()
    {
        Cursor.SetCursor(clickCursor, new Vector2(clickCursor.width / 3 , 0), CursorMode.Auto);

    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor, new Vector2(0, 0), CursorMode.Auto);
    }

    public void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void PlayButtonClickSound()
    {
        if (sound != null)
            sound.Play_No_Isplay("Click");
    }
}
