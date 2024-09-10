using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // The target that the camera will follow (the boat)
    public Vector3 offset;              // Offset from the target's position
    public float smoothSpeed = 0.125f;  // Smoothing speed for camera movement
    public float rotationSmoothSpeed = 5f; // Smoothing speed for camera rotation

    void LateUpdate()
    {
        // Smoothly move the camera towards the desired position
        FollowPosition();

        // Make the camera always look at the player (target)
        transform.LookAt(target);
    }

    private void FollowPosition()
    {
        // Calculate the desired position with offset
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}
