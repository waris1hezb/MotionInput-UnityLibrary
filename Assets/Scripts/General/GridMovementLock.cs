using UnityEngine;

public class GridMovementLock : MonoBehaviour
{
    public Transform magnetic;

    private void Update()
    {
        Vector3 pos = magnetic.position;
        Vector3 point = BrickGrid.Instance.GetNearestPointOnGrid(pos);
        transform.position = point;
    }
}
