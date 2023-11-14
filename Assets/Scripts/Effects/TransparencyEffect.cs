using System.Collections;
using UnityEngine;

public class TransparencyEffect : BaseEffect
{
    [Range(0, 255)]
    [SerializeField] private int _transparency = 60;

    private Renderer _renderer;

    private Material _originalMat;
    private Material _mat;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalMat = _renderer.material;

        _mat = Resources.Load("mat_transparent", typeof(Material)) as Material;
        
        if (_mat == null)
            return;

        Color color = _originalMat.color;
        color.a = _transparency / 255.0f;
        _mat.color = color;

        _mat = Instantiate(_mat);
    }

    public override void DisableEffect()
    {
        _renderer.material = _originalMat;
    }

    public override void EnableEffect(bool temp = false)
    {
        if (_mat == null)
            return;

        _renderer.material = _mat;

        base.EnableEffect(temp);
    }
}
