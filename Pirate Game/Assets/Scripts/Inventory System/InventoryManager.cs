using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public List<ItemSO> playerInventory = new List<ItemSO>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object between scenes
        } 
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddItemToInventory(ItemSO item)
    {
        playerInventory.Add(item);
        Debug.Log("Added " + item.itemName + " to the inventory.");

        // Optionally, you can update the inventory UI here
        //InventoryUI.Instance.UpdateInventoryUI();
    }
}
