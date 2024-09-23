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

    [SerializeField] private float drag = 1f;
    private bool isMovingForward = false;

    [SerializeField] private GameObject playerVisual;
    [SerializeField] private float tiltAngle = 15f;

    [SerializeField] private float interactRange;

    [SerializeField] private GameObject ropePrefab;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Adjust speed based on wind direction
        AdjustSpeedBasedOnWind();

        // Get input from user
        float moveForward = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        //// Apply drag if no forward input is given
        //if (moveForward > 0)
        //{
        //    // When moving forward, adjust speed based on wind
        //    AdjustSpeedBasedOnWind();
        //    isMovingForward = true;
        //}
        //else
        //{
        //    // If no input, apply drag to the current speed
        //    //ApplyDrag();
        //    isMovingForward = false;
        //}

        // Calculate movement and rotation
        Vector3 movement = transform.forward * moveForward * speed * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f, turn * rotationSpeed * Time.fixedDeltaTime, 0f);

        // Apply movement and rotation
        rb.MovePosition(rb.position + movement);
        rb.MoveRotation(rb.rotation * rotation);

        TiltPlayerVisual(turn);

        HandleDetection();
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
    //private void ApplyDrag()
    //{
    //    Gradually reduce speed by applying drag when the forward button is released
    //    if (!isMovingForward)
    //    {
    //        speed -= drag * Time.fixedDeltaTime;

    //        Ensure speed doesn't go below minSpeed
    //        if (speed < minSpeed)
    //        {
    //            speed = minSpeed;
    //        }
    //    }
    //}
    private void HandleDetection()
    {
        // Find all colliders within the interactRange radius

        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);

        foreach (Collider collider in colliders)
        {
            // check if the object has an IInteractable componnent
            IInteratable interactable = collider.GetComponent<IInteratable>();
            if (interactable != null)
            {
                interactable.Interact(transform);
                return;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
