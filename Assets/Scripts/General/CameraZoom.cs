using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _movementTime = 1.0f;
    [SerializeField] private float _speed = 5.0f;

    private Camera _cam;
    private CameraZoomEffect _cameraZoomEffect;

    private Vector3 _newZoom;
    private bool _effectEnabled = false;
    
    private int _scrollDirection = 1;
    private int _prevDirection = 1;

    private void Awake()
    {
        _cam = Camera.main;
        _cameraZoomEffect = GetComponent<CameraZoomEffect>();

        _newZoom = _cam.transform.localPosition;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
            return;
        if (Input.GetMouseButtonUp(1))
            _newZoom = _cam.transform.localPosition;

        HandleMouseInput();
        transform.localPosition = Vector3.Lerp(transform.position, _newZoom, Time.deltaTime * _movementTime);

        if (transform.localPosition != _newZoom && !_effectEnabled)
        {
            _effectEnabled = true;
            EnableAllEffects(_scrollDirection);
        }
        else if (_prevDirection != _scrollDirection)
        {
            EnableAllEffects(_scrollDirection);
        }
        else if (transform.localPosition == _newZoom && _effectEnabled)
        {
            _effectEnabled = false;
            DisableAllEffects();
        }

        _prevDirection = _scrollDirection;
    }

    private void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            _scrollDirection = (int)Mathf.Sign(Input.mouseScrollDelta.y);
            _newZoom = _newZoom + (Input.mouseScrollDelta.y * _cam.transform.forward * _speed);
        }
    }

    private void EnableAllEffects(int direction)
    {
        if (_cameraZoomEffect != null)
            _cameraZoomEffect.EnableEffect(direction);
    }

    private void DisableAllEffects()
    {
        if (_cameraZoomEffect != null)
            _cameraZoomEffect.DisableEffect();
    }
}
