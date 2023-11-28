using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UsableItemState", menuName = "Inventory/ItemState/UsableItemState")]
[System.Serializable]
public class UsableItemState : ItemState
{
    public int Dur_now;
}
