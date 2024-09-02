using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // The target that the camera will follow (the boat)
    public Vector3 offset;          // Offset from the target's position
    public float smoothSpeed = 0.125f; // Smoothing speed for camera movement

    void LateUpdate()
    {
        // Desired position for the camera
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        // Make sure the camera is always looking at the target
        transform.LookAt(target);
    }
}
