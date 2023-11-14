using UnityEngine;

public class DragInteraction : BaseInteraction
{
    private Camera _cam;
    private Vector3 _offset;
    private float _zCoord;

    private Vector3 _lastPos;

    protected override void Awake()
    {
        base.Awake();

        _cam = Camera.main;
    }

    private void OnMouseDown()
    {
        _zCoord = _cam.WorldToScreenPoint(transform.position).z;
        _offset = transform.position - GetMouseWorldPos();

        _lastPos = transform.position;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _zCoord;

        return _cam.ScreenToWorldPoint(mousePos);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + _offset;

        if (transform.position != _lastPos)
        {
            EnableAllEffects();
        }
        else
        {
            DisableAllEffects();
        }

        _lastPos = transform.position;
    }
}
