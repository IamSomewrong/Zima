using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Placeble Item", menuName = "Inventory/Item/PlacebleItem")]
[System.Serializable]
public class PlacebleItem : OneTimeItem
{
    public GameObject obj_to_place;
}
