using UnityEngine;

[DisallowMultipleComponent]
public class GlowEffect : BaseEffect
{
    [SerializeField] private float _intensity = 4.0f;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.DisableKeyword("_EMISSION");
        _renderer.material.SetVector("_EmissionColor", _renderer.material.color * Mathf.Pow(2, _intensity));
    }

    public override void DisableEffect()
    {
        _renderer.material.DisableKeyword("_EMISSION");
    }

    public override void EnableEffect(bool temp = false)
    {
        _renderer.material.EnableKeyword("_EMISSION");

        base.EnableEffect(temp);
    }
}
