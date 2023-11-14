using UnityEngine;

public class RotationInteraction : BaseInteraction
{
    [SerializeField] private float _speed = 10.0f;

    private Camera _cam;

    protected override void Awake()
    {
        base.Awake();

        _cam = Camera.main;
    }

    private void OnMouseDown()
    {
        EnableAllEffects();
    }

    private void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * _speed;
        float rotY = Input.GetAxis("Mouse Y") * _speed;

        Vector3 right = Vector3.Cross(_cam.transform.up, transform.position - _cam.transform.position);
        Vector3 up = Vector3.Cross(transform.position - _cam.transform.position, right);

        transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
        transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;
    }

    private void OnMouseUp()
    {
        DisableAllEffects();
    }
}
