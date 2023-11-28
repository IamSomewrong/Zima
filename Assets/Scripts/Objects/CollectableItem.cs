using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectable Item", menuName = "Inventory/Item/CollectableItem")]
[System.Serializable]
public class CollectableItem : Item
{
    public int max_count;
}
