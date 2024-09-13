using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassUI : MonoBehaviour
{
    [SerializeField] private Image compass;
    [SerializeField] private WindDirection currentWindDirection;
    [SerializeField] private float rotationSpeed = 5f; // Speed at which the compass rotates

    private float targetAngle;
    private float currentAngle;

    private void Start()
    {
        // Initialize the current angle to the current compass rotation
        currentAngle = compass.rectTransform.eulerAngles.z;
    }
    private void Update()
    {
        // Get the current wind direction
        currentWindDirection = WindSystem.Instance.GetCurrentWindDirectionEnum();

        // Set the target angle based on the wind direction
        targetAngle = WindDirectionToAngle(currentWindDirection);

        // Smoothly rotate the compass
        SmoothRotateCompass();
    }
    private void SmoothRotateCompass()
    {
        // Smoothly interpolate between the current and target angle using Mathf.LerpAngle
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        // Apply the updated angle to the compass' rotation
        compass.rectTransform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
    private float WindDirectionToAngle(WindDirection windDirection)
    {
        switch (windDirection)
        {
            case WindDirection.North:
                return 0f;
            case WindDirection.NorthEast:
                return -45f;
            case WindDirection.East:
                return -90f;
            case WindDirection.SouthEast:
                return -135f;
            case WindDirection.South:
                return -180f;
            case WindDirection.SouthWest:
                return -225f;
            case WindDirection.West:
                return -270f;
            case WindDirection.NorthWest:
                return -315f;
            default:
                return 0f;
        }
    }
}
