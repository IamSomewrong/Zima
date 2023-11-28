using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryShow : MonoBehaviour
{
    public List<Image> slots;
    public TakeItems inventory;

    void Start()
    {
        inventory.Inv_changed += ShowItems;
        ShowItems();
    }
    void Update()
    {
        foreach(Image im in slots)
        {
            im.color = new Color(1, 1, 1);
        }
        slots[(int)inventory.index_hand].color = new Color(0.1f, 0.1f, 0.1f);
    }

    public void ShowItems()
    {
        for (int i = 0; i < inventory.inv_items.Length; i++)
        {
            if(inventory.inv_items[i].item != null)
            {
                slots[i].sprite = inventory.inv_items[i].item.Icon;
                if(inventory.inv_items[i] is CollectableItemState coll)
                {
                    slots[i].GetComponentInChildren<Text>().enabled = true;
                    slots[i].GetComponentInChildren<Text>().text = coll.count.ToString();
                }
                else
                {
                    slots[i].GetComponentInChildren<Text>().enabled = false;
                }
            }
            else
            {
                slots[i].sprite = null;
                slots[i].GetComponentInChildren<Text>().enabled = false;
            }
        }
    }

}
