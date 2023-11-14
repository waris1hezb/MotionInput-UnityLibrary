using DG.Tweening;
using UnityEngine;

[DisallowMultipleComponent]
public class PositionPunchEffect : BaseEffect
{
    [SerializeField] private Vector3 _punch = Vector3.up * 0.5f;
    [SerializeField] private float _duration = 1.0f;

    private Tween _tween = null;

    public override void DisableEffect()
    {
        // This method intentionally does nothing for this particular effect.
    }

    public override void EnableEffect(bool temp = false)
    {
        if (_tween == null)
            _tween = transform.DOPunchPosition(_punch, _duration).OnComplete(() => _tween = null);
    }
}
