using DG.Tweening;
using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class ColorFlickerEffect : BaseEffect
{
    [SerializeField] private float _duration = 0.1f;
    [SerializeField] private Color _flickerColor = Color.white;

    private Renderer _renderer;
    private Color _originalColor;

    private Color _targetColor;
    private Coroutine _flickerCoroutine = null;
    private Tween _tween = null;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;

        _targetColor = _flickerColor;
    }

    public override void DisableEffect()
    {
        if (_flickerCoroutine != null)
            StopCoroutine(_flickerCoroutine);

        _tween.Kill();
        _renderer.material.color = _originalColor;
        _flickerCoroutine = null;
    }

    public override void EnableEffect(bool temp = false)
    {
        if (_flickerCoroutine == null)
            _flickerCoroutine = StartCoroutine(Flicker());

        base.EnableEffect(temp);
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            _tween = _renderer.material.DOColor(_targetColor, _duration);

            yield return new WaitForSeconds(_duration);

            _targetColor = 
                _targetColor == _flickerColor ? _originalColor : _flickerColor;
        }
    }
}
