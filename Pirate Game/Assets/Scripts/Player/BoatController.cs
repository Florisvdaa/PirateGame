using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 100f;

    [SerializeField] private GameObject playerVisual;
    [SerializeField] private float tiltAngle = 15f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Get input from user
        float moveForward = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        // Calculate movemnet an rotation
        Vector3 movement = transform.forward * moveForward * speed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0f, turn * rotationSpeed * Time.deltaTime, 0f);

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
}
