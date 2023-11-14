using UnityEngine;

public class Billboard : MonoBehaviour
{
    public enum BillboardType 
    { 
        LookAtCamera, 
        CameraForward 
    };
    
    [SerializeField] private BillboardType _billboardType = BillboardType.CameraForward;

    [Header("Lock Rotation")]
    [SerializeField] private bool _lockX;
    [SerializeField] private bool _lockY;
    [SerializeField] private bool _lockZ;

    private Camera _cam;
    private Vector3 _initialRotation;

    private void Awake()
    {
        _cam = Camera.main;
        _initialRotation = transform.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        switch (_billboardType)
        {
            case BillboardType.LookAtCamera:
                transform.LookAt(_cam.transform.position, Vector3.up);
                break;
            case BillboardType.CameraForward:
                transform.forward = _cam.transform.forward;
                break;
            default:
                break;
        }

        Vector3 rotation = transform.rotation.eulerAngles;
        if (_lockX)
            rotation.x = _initialRotation.x;
        if (_lockY)
            rotation.y = _initialRotation.y;
        if (_lockZ)
            rotation.z = _initialRotation.z;
    }
}
