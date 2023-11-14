using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class OutlineFlickerEffect : BaseEffect
{
    [SerializeField] private Outline.Mode _outlineMode = Outline.Mode.OutlineVisible;
    [SerializeField] private Color _outlineColor = Color.black;
    [SerializeField] private Color _flickerColor = Color.white;
    [SerializeField] private float _outlineWidth = 5.0f;
    [SerializeField] private float _duration = 0.1f;

    private Outline _outline;
    private Color _originalColor;

    private Color _targetColor;
    private Coroutine _flickerCoroutine = null;

    private void Awake()
    {
        _outline = gameObject.AddComponent<Outline>();
        _outline.OutlineMode = _outlineMode;
        _outline.OutlineColor = _outlineColor;
        _outline.OutlineWidth = _outlineWidth;
        _outline.enabled = false;

        _originalColor = _outline.OutlineColor;
    }

    public override void DisableEffect()
    {
        if (_outline == null)
            return;

        _outline.enabled = false;
        if (_flickerCoroutine != null)
            StopCoroutine(_flickerCoroutine);

        _outline.OutlineColor = _originalColor;
        _flickerCoroutine = null;
    }

    public override void EnableEffect(bool temp = false)
    {
        if (_outline == null)
            return;

        _outline.enabled = true;
        if (_flickerCoroutine == null)
            _flickerCoroutine = StartCoroutine(Flicker());

        base.EnableEffect(temp);
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            _outline.OutlineColor = _targetColor;

            yield return new WaitForSeconds(_duration);

            _targetColor =
                _targetColor == _flickerColor ? _originalColor : _flickerColor;
        }
    }
}
