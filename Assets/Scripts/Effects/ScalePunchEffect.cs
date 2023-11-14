using DG.Tweening;
using UnityEngine;

public class ScalePunchEffect : BaseEffect
{
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _punch = 0.1f;
    [SerializeField] private float _duration = 1.0f;

    private Vector3 _initialScale;
    private Vector3 _punchScale;

    private Tween _tween = null;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _punchScale = (_initialScale * -_punch);
    }

    public override void DisableEffect()
    {
        // This method intentionally does nothing for this particular effect.
    }

    public override void EnableEffect(bool temp = false)
    {
        if (_tween == null)
            _tween = transform.DOPunchScale(_punchScale, _duration).OnComplete(() => _tween = null);
    }
}
