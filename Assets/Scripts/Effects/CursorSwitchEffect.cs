using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class CursorSwitchEffect : BaseEffect
{
    [SerializeField] private Texture2D _cursorTexture;
    [SerializeField] private CursorHotspot _hotspot = CursorHotspot.Center;

    private void Awake()
    {
        if (CursorManager.Instance == null)
        {
            GameObject cursorManager = new GameObject("CursorManager");
            cursorManager.AddComponent<CursorManager>();
        }
    }

    public override void DisableEffect()
    {
        CursorManager.Instance.RestorePreviousCursor(true);
    }

    public override void EnableEffect(bool temp = false)
    {
        if (_cursorTexture != null)
            CursorManager.Instance.SetCursor(new CustomCursor { texture = _cursorTexture, hotspot = _hotspot }, true);

        base.EnableEffect(temp);
    }
}
