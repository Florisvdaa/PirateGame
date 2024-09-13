using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDirection : MonoBehaviour
{
    [SerializeField] private Image playerDirection;  // UI Image for showing player direction
    [SerializeField] private Transform player;       // Reference to the player's transform
    [SerializeField] private float rotationSpeed = 5f; // Speed at which the direction rotates

    private float targetAngle;
    private float currentAngle;

    private void Start()
    {
        // Initialize the current angle to the current image rotation
        currentAngle = playerDirection.rectTransform.eulerAngles.z;
    }

    private void Update()
    {
        // Calculate the angle based on player's forward direction
        targetAngle = PlayerDirectionToAngle(player.forward);

        // Smoothly rotate the player direction image
        SmoothRotatePlayerDirection();
    }

    private void SmoothRotatePlayerDirection()
    {
        // Smoothly interpolate between the current and target angle using Mathf.LerpAngle
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        // Apply the updated angle to the player direction's rotation
        playerDirection.rectTransform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    private float PlayerDirectionToAngle(Vector3 playerForward)
    {
        // Get the angle in degrees between player's forward and the world forward (Z axis)
        float angle = Mathf.Atan2(playerForward.x, playerForward.z) * Mathf.Rad2Deg;

        // We negate the angle to match the clockwise rotation for the UI image
        return -angle;
    }
}
