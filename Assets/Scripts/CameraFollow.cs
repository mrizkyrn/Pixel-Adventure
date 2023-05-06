using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public Vector2 minPosition;
    public Vector2 maxPosition;

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Clamp the camera position
        float clampedX = Mathf.Clamp(smoothedPosition.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minPosition.y, maxPosition.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
