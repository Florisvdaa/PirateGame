using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemPool", menuName = "Inventory/Item Pool")]
public class ItemPoolSO : ScriptableObject
{
    public List<ItemSO> itemPool;
}
