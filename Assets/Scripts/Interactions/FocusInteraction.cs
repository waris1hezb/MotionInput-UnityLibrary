using UnityEngine;

public class FocusInteraction : BaseInteraction
{
    [SerializeField] private float _hoverDuration = 2.0f;
    
    private bool _isHovering = false;
    private bool _isFocused = false;
    private float _hoverTimer = 0.0f;

    private void OnMouseEnter()
    {
        _isHovering = true;
    }

    private void OnMouseOver()
    {
        if (_isHovering && !_isFocused)
        {
            _hoverTimer += Time.deltaTime;

            if (_hoverTimer >= _hoverDuration)
            {
                _isFocused = true;

                EnableAllEffects();
            }
        }
    }

    private void OnMouseExit()
    {
        _isHovering = false;
        _isFocused = false;
        _hoverTimer = 0.0f;

        DisableAllEffects();
    }
}
