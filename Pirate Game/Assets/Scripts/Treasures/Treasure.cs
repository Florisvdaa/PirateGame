using GogoGaga.OptimizedRopesAndCables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour, IInteratable
{
    [SerializeField] private ItemPoolSO itemPool;

    private bool isOpened = false;
    [SerializeField] private GameObject ropePrefab;
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private float minDistanceToPlayer = 2f; // Minimum distance to maintain from player
    [SerializeField] private int ropeLengthThreshold = 10; // Rope length threshold to start lerping
    [SerializeField] private float floatAmplitude = 0.5f; // Amplitude for floating effect
    [SerializeField] private float floatFrequency = 1f;   // Frequency for floating effect

    private Rope rope;
    [SerializeField] private bool hasRopeConnected = false;
    private Vector3 originalPosition;

    public void OpenChest()
    {
        // Add opening animation

        isOpened = true;

        // Get a random item from the item pool
        ItemSO randomItem = GetRandomItem();

        if (randomItem != null)
        {
            Debug.Log("You received: " + randomItem.itemName);

            // Add the random item to the player's inventory (you need an InventoryManager to handle this)
            InventoryManager.Instance.AddItemToInventory(randomItem);
        }
        else
        {
            Debug.Log("No items available in the pool.");
        }
    }

    private void ConnectRope(Transform playerTransform)
    {
        GameObject ropeObject = Instantiate(ropePrefab, playerTransform.position, Quaternion.identity);
        rope = ropeObject.GetComponent<Rope>();
        rope.SetStartPoint(playerTransform);
        rope.SetEndPoint(this.transform);
        hasRopeConnected = true;
    }

    public void Interact(Transform playerTransform)
    {
        // Create a UI that shows when you are in range to handle input
        if (Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            OpenChest();
        }

        //if (Input.GetKeyDown(KeyCode.E) && !hasRopeConnected)
        //{
        //    //OpenChest();
        //    ConnectRope(playerTransform);
        //}
    }

    private void Update()
    {
        if (hasRopeConnected && rope.GetRopeLength() > ropeLengthThreshold)
        {
            FloatTowardsPlayer(rope.GetRopeStart().position);
        }
    }

    private void FloatTowardsPlayer(Vector3 playerPosition)
    {
        // Calculate the direction toward the player
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

        // Move towards the player only if the treasure is farther than the minimum distance
        if (distanceToPlayer > minDistanceToPlayer)
        {
            // Lerp the treasure's position toward the player, maintaining the minimum distance
            Vector3 targetPosition = playerPosition - directionToPlayer * minDistanceToPlayer;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);

            // Apply floating effect on the Y-axis
            float floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, transform.position.y + floatOffset, transform.position.z);
        }
    }

    private ItemSO GetRandomItem()
    {
        if (itemPool.itemPool.Count == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, itemPool.itemPool.Count);
        return itemPool.itemPool[randomIndex];
    }
}
