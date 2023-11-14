using UnityEngine;

public class DragGridInteraction : BaseInteraction
{
    [Range(0, 255)]
    [SerializeField] private int _ghostTransparency = 60;

    private Camera _cam;
    private Vector3 _offset;
    private float _zCoord;

    private Vector3 _lastPos;

    private GameObject _ghost;

    protected override void Awake()
    {
        base.Awake();

        _cam = Camera.main;
        if (BrickGrid.Instance == null)
        {
            GameObject grid = new GameObject("Grid");
            grid.AddComponent<BrickGrid>();
        }
        InitGhost();

        transform.position = BrickGrid.Instance.GetNearestPointOnGrid(transform.position);
    }

    private void OnMouseDown()
    {
        _zCoord = _cam.WorldToScreenPoint(transform.position).z;
        _offset = transform.position - GetMouseWorldPos();

        _lastPos = transform.position;
    }

    private void InitGhost()
    {
        _ghost = new GameObject();

        // adding components to ghost
        MeshRenderer meshRenderer = _ghost.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = _ghost.AddComponent<MeshFilter>();

        // setting components properties for ghost
        meshRenderer.material = GetComponent<MeshRenderer>().material;
        meshFilter.mesh = GetComponent<MeshFilter>().mesh;

        GridMovementLock gridLock = _ghost.AddComponent<GridMovementLock>();
        gridLock.magnetic = transform;

        _ghost.transform.SetParent(transform);
        _ghost.SetActive(false);

        Material material = Resources.Load("mat_transparent", typeof(Material)) as Material;
        if (material != null)
        {
            material = Instantiate(material);
            Color color = meshRenderer.material.color;
            meshRenderer.material = material;
            color.a = _ghostTransparency / 255.0f;
            material.color = color;
        }
        else
            meshRenderer.material.color = Color.grey;
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
            _ghost.SetActive(true);
        }
        else
        {
            DisableAllEffects();
        }

        _lastPos = transform.position;
    }

    private void OnMouseUp()
    {
        _ghost.SetActive(false);
        transform.position = _ghost.transform.position;
    }
}
