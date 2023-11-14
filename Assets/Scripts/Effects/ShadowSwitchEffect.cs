using UnityEngine;
using UnityEngine.Rendering;

[DisallowMultipleComponent]
public class ShadowSwitchEffect : BaseEffect
{
    [SerializeField] private ShadowCastingMode _shadowMode;

    private Renderer _renderer;
    private ShadowCastingMode _originalMode;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalMode = _renderer.shadowCastingMode;
    }

    public override void DisableEffect()
    {
        _renderer.shadowCastingMode = _originalMode;
    }

    public override void EnableEffect(bool temp = false)
    {
        _renderer.shadowCastingMode = _shadowMode;

        base.EnableEffect(temp);
    }
}
