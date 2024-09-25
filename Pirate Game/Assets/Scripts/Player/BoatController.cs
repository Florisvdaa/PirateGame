using GogoGaga.OptimizedRopesAndCables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField] private float speed = 25f;
    [SerializeField] private float rotationSpeed = 100f;

    [SerializeField] private float maxSpeed = 40f;  // Maximum speed the boat can reach
    [SerializeField] private float minSpeed = 15f;   // Minimum speed the boat can reach

    [SerializeField] private GameObject playerVisual;
    [SerializeField] private float tiltAngle = 15f;

    [SerializeField] private float interactRange;
    [SerializeField] private GameObject ropePrefab;

    private Rigidbody rb;
    private IInteratable lastInteractable = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleDetection();
    }
    private void FixedUpdate()
    {
        // Adjust speed based on wind direction
        AdjustSpeedBasedOnWind();

        // Get input from user
        float moveForward = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        // Calculate movement and rotation
        Vector3 movement = transform.forward * moveForward * speed * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f, turn * rotationSpeed * Time.fixedDeltaTime, 0f);

        // Apply movement and rotation
        rb.MovePosition(rb.position + movement);
        rb.MoveRotation(rb.rotation * rotation);

        TiltPlayerVisual(turn);
    }

    private void TiltPlayerVisual(float turn)
    {
        // Only tilt the visual on Z-axis when turning
        if (turn != 0)
        {
            float tiltZ = -turn * tiltAngle; // Rotate on Z-axis when turning
            playerVisual.transform.localRotation = Quaternion.Euler(0f, 0f, tiltZ);
        }
        else
        {
            // Reset the visual to a neutral Z rotation when not turning
            playerVisual.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void AdjustSpeedBasedOnWind()
    {
        if (WindSystem.Instance != null)
        {
            // Get the wind direction and strength
            Vector3 windDirection = WindSystem.Instance.GetWindDirection();
            float windStrength = WindSystem.Instance.GetWindStrength();

            // Calculate the dot product to determine how aligned the boat is with the wind
            float alignment = Vector3.Dot(transform.forward, windDirection.normalized);

            // Calculate the speed change based on wind strength and alignment
            float speedChange = alignment * windStrength * Time.fixedDeltaTime;

            // Update speed based on calculated speed change
            speed += speedChange;

            // Clamp the speed between minSpeed and maxSpeed
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        }
    }

    private void HandleDetection()
    {
        // Find all colliders within the interactRange radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);

        IInteratable currentInteractable = null;

        foreach (Collider collider in colliders)
        {
            // Check if the object has an IInteractable component
            IInteratable interactable = collider.GetComponent<IInteratable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                interactable.Interact(transform);
                interactable.EnableUI();
                break; // Found one, so no need to check other colliders
            }
        }

        // Handle when the player leaves the interact range of the previous interactable
        if (lastInteractable != null && currentInteractable != lastInteractable)
        {
            lastInteractable.DisableUI();
        }

        // Update the last interactable object to the current one
        lastInteractable = currentInteractable;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
