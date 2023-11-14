using DG.Tweening;
using UnityEngine;

public class RotationPunchEffect : BaseEffect
{
    [SerializeField] private Vector3 _punch = new Vector3(10.0f, 10.0f, 10.0f);
    [SerializeField] private float _duration = 1.0f;

    private Vector3 _initialRotation;
    private Vector3 _punchRotation;

    private Tween _tween = null;

    private void Awake()
    {
        _initialRotation = transform.rotation.eulerAngles;
        _punchRotation = _initialRotation + _punch;
    }

    public override void DisableEffect()
    {
        // This method intentionally does nothing for this particular effect.
    }

    public override void EnableEffect(bool temp = false)
    {
        if (_tween == null)
            _tween = transform.DOPunchRotation(_punchRotation, _duration).OnComplete(() => _tween = null);
    }
}
