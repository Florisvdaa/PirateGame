using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySaveLoad : MonoBehaviour
{
    public string PLAYERINVENTORY = "PlayerInventory";
    //public void SaveInventory()
    //{
    //    string json = JsonUtility.ToJson(InventoryManager.Instance.playerInventory);
    //    PlayerPrefs.SetString(PLAYERINVENTORY, json);
    //    PlayerPrefs.Save();
    //}

    //public void LoadInventory()
    //{
    //    if (PlayerPrefs.HasKey("PlayerInventory"))
    //    {
    //        string json = PlayerPrefs.GetString(PLAYERINVENTORY);
    //        InventoryManager.Instance.playerInventory = JsonUtility.FromJson<Inventory>(json);
    //    }
    //}
}