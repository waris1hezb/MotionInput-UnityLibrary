using System.Collections.Generic;
using UnityEngine;

public enum CursorHotspot
{
    Default,
    Center
}

public class CustomCursor
{
    public Texture2D texture;
    public CursorHotspot hotspot;
}

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    private Stack<CustomCursor> _previousCursors = new Stack<CustomCursor>();
    private bool locked = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _previousCursors.Push(new CustomCursor { texture = null, hotspot = CursorHotspot.Default });
    }

    public void SetCursor(CustomCursor customCursor, bool lockCursor = false)
    {
        if (locked)
            return;

        Vector2 hotspot;
        if (customCursor.hotspot == CursorHotspot.Center)
        {
            hotspot = new Vector2(customCursor.texture.width / 2, customCursor.texture.height / 2);
        }
        else
        {
            hotspot = Vector2.zero;
        }

        _previousCursors.Push(customCursor);
        Cursor.SetCursor(customCursor.texture, hotspot, CursorMode.Auto);
        locked = lockCursor;
        // PrintStack();
    }

    public void RestorePreviousCursor(bool releaseLock = false)
    {
        if (releaseLock)
            locked = false;

        if (locked)
            return;

        if (_previousCursors.Count == 1)
        {
            CustomCursor customCursor = _previousCursors.Peek();
            Cursor.SetCursor(customCursor.texture, customCursor.hotspot == CursorHotspot.Center ?
                new Vector2(customCursor.texture.width / 2, customCursor.texture.height / 2) : Vector2.zero, CursorMode.Auto);
        }
        else if (_previousCursors.Count > 0)
        {
            _previousCursors.Pop();
            CustomCursor customCursor = _previousCursors.Peek();
            Cursor.SetCursor(customCursor.texture, customCursor.hotspot == CursorHotspot.Center ?
                new Vector2(customCursor.texture.width / 2, customCursor.texture.height / 2) : Vector2.zero, CursorMode.Auto);
        }
    }

    private void PrintStack()
    {
        Debug.Log("Current stack contents:");
        foreach (var item in _previousCursors)
        {
            Debug.Log(item.texture);
            Debug.Log(item.hotspot);
        }
    }
}
