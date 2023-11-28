using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemState", menuName = "Inventory/ItemState")]
[System.Serializable]
public class ItemState : ScriptableObject
{
    public Item item;
}


