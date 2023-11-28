using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISlot : MonoBehaviour, IDropHandler
{
    InventoryShow invShow;
    GameObject itemOfSlot;
    public Slot slot;
    
    private void Start()
    {
        invShow = GetComponentInParent<InventoryShow>();
        itemOfSlot = transform.GetChild(0).gameObject;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject item = eventData.pointerDrag;
        int from = invShow.slots.IndexOf(item.GetComponent<Image>());
        if (slot == Slot.None)
        {
            int to = invShow.slots.IndexOf(itemOfSlot.GetComponent<Image>());
            if (invShow.inventory.inv_items[from] != invShow.inventory.inv_items[8] || 
                (invShow.inventory.inv_items[from] == invShow.inventory.inv_items[8] && ((invShow.inventory.inv_items[from].item is UsableItem uif && 
                invShow.inventory.inv_items[to].item is UsableItem uit && uif.slot == uit.slot) || invShow.inventory.inv_items[to].item == null))) 
            {
                ItemState tempState = invShow.inventory.inv_items[from];
                invShow.inventory.inv_items[from] = invShow.inventory.inv_items[to];
                invShow.inventory.inv_items[to] = tempState;
                invShow.inventory.Inv_changed?.Invoke();
            }
        }
        else if(slot == Slot.Head)
        {
            if(invShow.inventory.inv_items[from].item is UsableItem uitem && uitem.slot == Slot.Head)
            {
                int to = 8; // Слот Шлема
                ItemState tempState = invShow.inventory.inv_items[from];
                invShow.inventory.inv_items[from] = invShow.inventory.inv_items[to];
                invShow.inventory.inv_items[to] = tempState;
                invShow.inventory.Inv_changed?.Invoke();
            }
        }
    }
}
