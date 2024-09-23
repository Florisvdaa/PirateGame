using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        StartCoroutine(DestroyAfterLifeTime());
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    // Check if the object hit has a health component (or tag)
    //    if (other.CompareTag("Enemy")) // Assuming the target has a tag "Enemy"
    //    {
    //        // Here you would call a function on the enemy object to apply damage
    //        Enemy enemy = other.GetComponent<Enemy>();
    //        if (enemy != null)
    //        {
    //            enemy.TakeDamage(damage);
    //        }

    //        // Destroy the projectile after hitting something
    //        Destroy(gameObject);
    //    }
    //    else if (other.CompareTag("Obstacle")) // Example for hitting obstacles
    //    {
    //        Destroy(gameObject); // Destroy on impact with an obstacle
    //    }
    //}

    private IEnumerator DestroyAfterLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
