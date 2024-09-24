using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory 
{
    public List<ItemSO> items = new List<ItemSO>();

    public void AddItem(ItemSO item)
    {
        items.Add(item);
    }

    public void RemoveItem(ItemSO item)
    {
        items.Remove(item);
    }
}
