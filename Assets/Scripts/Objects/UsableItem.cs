using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new UsableItem", menuName = "Inventory/Item/UsableItem")]
[System.Serializable]
public class UsableItem : Item
{

    public Slot slot;
    public int Durability;
    public bool ForWood;
}

public enum Slot
{
    None,
    Head
}
