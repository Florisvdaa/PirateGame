using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform leftAimStartTransform; // Starting position for the left aim
    [SerializeField] private Transform leftAimEndTransform;   // End position for the left aim
    [SerializeField] private Transform rightAimStartTransform; // Starting position for the right aim
    [SerializeField] private Transform rightAimEndTransform;   // End position for the right aim

    [SerializeField] private float leftMaxDistance = -100f;    // Max distance for the left aim
    [SerializeField] private float rightMaxDistance = 100f;  // Max distance for the right aim (negative direction)
    [SerializeField] private float moveSpeed = 15f;             // Speed at which the transforms move

    private bool isAiming = false;     // Track whether the right mouse button is held
    [SerializeField] private bool isLeftAim = true;     // Bool to check if left or right aim is selected (true for left, false for right)

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button pressed
        {
            isAiming = true;
        }

        if (Input.GetMouseButtonUp(1)) // Right mouse button released
        {
            isAiming = false;
        }

        // Toggle between left and right aim with some input (e.g., press 'L' for left, 'R' for right)
        if (Input.GetKeyDown(KeyCode.L)) // Press 'L' to aim left
        {
            isLeftAim = true;
        }
        if (Input.GetKeyDown(KeyCode.R)) // Press 'R' to aim right
        {
            isLeftAim = false;
        }

        // Handle aiming logic
        if (isAiming)
        {
            if (isLeftAim)
            {
                MoveEndTransformAway(leftAimStartTransform, leftAimEndTransform, leftMaxDistance);
            }
            else
            {
                MoveEndTransformAway(rightAimStartTransform, rightAimEndTransform, rightMaxDistance);
            }
        }
        else
        {
            if (isLeftAim)
            {
                MoveEndTransformBack(leftAimStartTransform, leftAimEndTransform);
            }
            else
            {
                MoveEndTransformBack(rightAimStartTransform, rightAimEndTransform);
            }
        }
    }

    private void MoveEndTransformAway(Transform startTransform, Transform endTransform, float maxDistance)
    {
        if (startTransform != null && endTransform != null)
        {
            // Calculate the current distance between start and end transforms
            float currentDistance = Vector3.Distance(endTransform.position, startTransform.position);

            // Check if the end transform has reached or exceeded the max distance
            if (Mathf.Abs(currentDistance) < Mathf.Abs(maxDistance))
            {
                // Move the end transform in the correct direction of the start transform (X axis movement)
                Vector3 direction = (maxDistance > 0) ? startTransform.right : -startTransform.right; // Use positive or negative X direction
                endTransform.position += direction * moveSpeed * Time.deltaTime;

                // Clamp the position so that the end transform doesn't exceed the max distance
                currentDistance = Vector3.Distance(endTransform.position, startTransform.position);
                if (Mathf.Abs(currentDistance) > Mathf.Abs(maxDistance))
                {
                    endTransform.position = startTransform.position + direction * Mathf.Abs(maxDistance);
                }
            }
        }
    }

    private void MoveEndTransformBack(Transform startTransform, Transform endTransform)
    {
        if (startTransform != null && endTransform != null)
        {
            // Move the end transform smoothly back to the start position
            endTransform.position = Vector3.MoveTowards(endTransform.position, startTransform.position, moveSpeed * Time.deltaTime);
        }
    }
}
