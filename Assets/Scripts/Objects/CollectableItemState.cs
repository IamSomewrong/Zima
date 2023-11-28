using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CollectableItemState", menuName = "Inventory/ItemState/CollectableItemState")]
[System.Serializable]
public class CollectableItemState : ItemState
{
    public int count = 0;
}
