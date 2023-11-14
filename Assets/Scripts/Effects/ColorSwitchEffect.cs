using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ColorSwitchEffect : BaseEffect
{
    [SerializeField] private float _duration = 0.1f;
    [SerializeField] private Color _color = Color.blue;

    private Renderer _renderer;
    private Color _originalColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
    }

    public override void DisableEffect()
    {
        _renderer.material.DOColor(_originalColor, _duration);
    }

    public override void EnableEffect(bool temp = false)
    {
        _renderer.material.DOColor(_color, _duration);

        base.EnableEffect(temp);
    }
}
