using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 100f;

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
    }
}
