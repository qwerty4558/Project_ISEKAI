using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManage : MonoBehaviour
{
    [SerializeField] Texture2D clickCursor;
    [SerializeField] Texture2D defaultCursor;

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
}
