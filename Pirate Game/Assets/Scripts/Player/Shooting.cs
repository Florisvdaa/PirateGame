using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform leftAimStartTransform;  // Starting position for the left aim
    [SerializeField] private Transform rightAimStartTransform; // Starting position for the right aim

    [SerializeField] private GameObject projectilePrefab;      // Prefab of the projectile to shoot
    [SerializeField] private float projectileSpeed = 20f;      // Speed of the projectile

    [SerializeField] private GameObject leftActivationObject;  // GameObject for the left side
    [SerializeField] private GameObject rightActivationObject; // GameObject for the right side


    private void Update()
    {
        // Handle shooting and activating objects when left or right mouse button is pressed
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            ActivateSide(true);    // Activate left side and deactivate right side
            ShootProjectile(true); // Shoot to the left
        }
        if (Input.GetMouseButtonDown(1)) // Right mouse button pressed
        {
            ActivateSide(false);    // Activate right side and deactivate left side
            ShootProjectile(false); // Shoot to the right
        }
    }

    private void ShootProjectile(bool isLeftShoot)
    {
        Transform aimStartTransform = isLeftShoot ? leftAimStartTransform : rightAimStartTransform;

        if (aimStartTransform != null && projectilePrefab != null)
        {
            // Instantiate the projectile at the aimStartTransform's position and rotation
            GameObject projectile = Instantiate(projectilePrefab, aimStartTransform.position, aimStartTransform.rotation);

            // Apply velocity to the projectile in the direction (right for right side, left for left side)
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Shoot to the right for right aim, or to the left for left aim
                Vector3 direction = isLeftShoot ? -aimStartTransform.right : aimStartTransform.right;
                rb.velocity = direction * projectileSpeed;
            }
        }
    }

    private void ActivateSide(bool isLeftSide)
    {
        // Activate the left side object and deactivate the right side if shooting left
        // Activate the right side object and deactivate the left side if shooting right
        if (isLeftSide)
        {
            if (leftActivationObject != null)
                leftActivationObject.SetActive(true);

            if (rightActivationObject != null)
                rightActivationObject.SetActive(false);
        }
        else
        {
            if (rightActivationObject != null)
                rightActivationObject.SetActive(true);

            if (leftActivationObject != null)
                leftActivationObject.SetActive(false);
        }
    }
}
