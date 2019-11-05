using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{

    public Texture2D pickingGemsCursor;
    public Texture2D defaultCursor;
    private Vector2 hotspot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;

    // Use this for initialization
    void Start()
    {

    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(pickingGemsCursor, hotspot,cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor, hotspot, cursorMode);
    }
}
