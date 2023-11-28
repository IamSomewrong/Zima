using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Craft/Recipe")]
[System.Serializable]
public class Recipe : ScriptableObject
{
    public Item[] from_items;
    public Item to_item;
}
