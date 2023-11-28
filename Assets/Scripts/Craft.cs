using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    public Recipe recipe;
    private TakeItems inventory;
    private List<ItemState> it_craft;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<TakeItems>();
        Get_It_Craft();
    }

    public void Craft_It()
    {
        bool craftable = true;
        for (int i = 0; i < it_craft.Count; i++)
        {
            craftable &= Check(it_craft[i]);
        }
        if (craftable)
        {
            for (int i = 0; i < it_craft.Count; i++)
            {
                for (int j = 0; j < inventory.inv_items.Length; j++)
                {
                    if(inventory.inv_items[j].item == it_craft[i].item && ((CollectableItemState)inventory.inv_items[j]).count >= ((CollectableItemState)it_craft[i]).count)
                    {
                        for (int k = 0; k < ((CollectableItemState)it_craft[i]).count; k++)
                        {
                            inventory.ConsumeItem(inventory.inv_items[j]);
                        }
                    }
                }
            }
            inventory.Add_to_Inv(recipe.to_item);
        }
    }

    private bool Check(ItemState item)
    {
        for (int i = 0; i < inventory.inv_items.Length; i++)
        {
            if(inventory.inv_items[i].item == item.item && ((CollectableItemState)inventory.inv_items[i]).count >= ((CollectableItemState)item).count)
            {
                return true;
            }
        }
        return false;
    }

    private void Get_It_Craft()
    {
        it_craft = new List<ItemState>();
        foreach (Item it in recipe.from_items)
        {
            if (it_craft.Count == 0)
            {
                if (it is CollectableItem coll_it)
                {
                    CollectableItemState col_st = ScriptableObject.CreateInstance<CollectableItemState>();
                    col_st.item = coll_it;
                    col_st.count = 1;
                    it_craft.Add(col_st);
                }
            }
            else
            {
                if (it is CollectableItem coll_it)
                {
                    int state = -1;
                    for (int i = 0; i < it_craft.Count; i++)
                    {
                        if (it_craft[i].item == coll_it)
                        {
                            state = i;
                        }
                    }
                    if (state == -1)
                    {
                        CollectableItemState col_st = ScriptableObject.CreateInstance<CollectableItemState>();
                        col_st.item = coll_it;
                        col_st.count = 1;
                        it_craft.Add(col_st);
                    }
                    else
                    {
                        ((CollectableItemState)it_craft[state]).count++;
                    }
                }
            }
        }
    }
}
