using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Assign Mario's transform in the Inspector
    public Vector3 offset = new Vector3(0, 2, -10); // Adjust the offset as needed
    public float smoothSpeed = 0.125f; // Adjust the smoothness of the camera movement

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.y = transform.position.y; // Lock the Y position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}