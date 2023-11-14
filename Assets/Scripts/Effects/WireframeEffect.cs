using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class WireframeEffect : BaseEffect
{
    public enum WireframeType
    {
        TransparentWireframe,
        TransparentCulledWireframe,
        SolidWireframe
    }

    [SerializeField] private WireframeType wireframeType = WireframeType.TransparentWireframe;
    [SerializeField] private Color _wireColor = Color.magenta;
    [SerializeField] private Color _baseColor = Color.black;
    [Range(0, 800)]
    [SerializeField] private float _wireThickness = 600;
    [Range(0, 20)]
    [SerializeField] private float _wireSmoothness = 3;

    private Material _wireframeMat;

    private Renderer _renderer;
    private Material _originalMat;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalMat = _renderer.material;

        InitWireframeMaterial();
    }

    public override void DisableEffect()
    {
        _renderer.material = _originalMat;
    }

    public override void EnableEffect(bool temp = false)
    {
        if (_wireframeMat != null)
            _renderer.material = _wireframeMat;

        base.EnableEffect(temp);
    }

    private void InitWireframeMaterial()
    {
        string materialName;

        switch (wireframeType)
        {
            case WireframeType.TransparentWireframe:
                materialName = "mat_wireframe_transparent";
                break;
            case WireframeType.TransparentCulledWireframe:
                materialName = "mat_wireframe_transparent_culled";
                break;
            case WireframeType.SolidWireframe:
                materialName = "mat_wireframe_solid";
                break;
            default:
                materialName = "mat_wireframe_transparent"; // Default to TransparentWireframe
                break;
        }

        _wireframeMat = Resources.Load(materialName, typeof(Material)) as Material;

        if (_wireframeMat == null)
            return;

        _wireframeMat = Instantiate(_wireframeMat);
        _wireframeMat.SetColor("_WireColor", _wireColor);
        _wireframeMat.SetColor("_BaseColor", _baseColor);
        _wireframeMat.SetFloat("_WireThickness", _wireThickness);
        _wireframeMat.SetFloat("_WireSmoothness", _wireSmoothness);
    }
}
