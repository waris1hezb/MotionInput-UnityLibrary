using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class OutlineEffect : BaseEffect
{
    [SerializeField] private Outline.Mode _outlineMode = Outline.Mode.OutlineVisible;
    [SerializeField] private Color _outlineColor = Color.black;
    [SerializeField] private float _outlineWidth = 5.0f;

    private Outline _outline;

    private void Awake()
    {
        _outline = gameObject.AddComponent<Outline>();
        _outline.OutlineMode = _outlineMode;
        _outline.OutlineColor = _outlineColor;
        _outline.OutlineWidth = _outlineWidth;
        _outline.enabled = false;
    }

    public override void DisableEffect()
    {
        if (_outline != null)
            _outline.enabled = false;
    }

    public override void EnableEffect(bool temp = false)
    {
        if (_outline != null)
            _outline.enabled = true;

        base.EnableEffect(temp);
    }
}
