using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticMovementLock : MonoBehaviour
{
    public Transform magnetic;
    public float height;
    public float otherY;

    private void Update()
    {
        Vector3 pos = magnetic.position;
        pos.y = otherY + height;
        Vector3 point = BrickGrid.Instance.GetNearestPointOnGrid(pos);
        transform.position = point;
    }
}
