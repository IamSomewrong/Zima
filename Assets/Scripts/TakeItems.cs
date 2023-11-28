using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TakeItems : MonoBehaviour
{
    public UnityAction Inv_changed;
    public int inv_size;
    public ItemState[] inv_items;
    public SpriteRenderer hand_sprite;
    public ItemState inhand;
    public float index_hand;
    public List<Item> StartItems;
    Build b_system;

    private void Start()
    {
        b_system = GetComponent<Build>();
        inv_items = new ItemState[inv_size + 1];
        inv_items = Camera.main.GetComponent<General>().inventory;
        Inv_changed?.Invoke();
        inhand = inv_items[0];
        index_hand = 0;
        if (inhand.item == null)
        {
            hand_sprite.sprite = null;
        }
        else
        {
            hand_sprite.sprite = inhand.item.Icon;
        }
    }
    private void Update()
    {
        SwitchHand();
        DestroyTool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            if (Add_to_Inv(((Take)(collision.gameObject.GetComponent("Take"))).taken()))
            {
                GameObject.Destroy(collision.gameObject);
            }
        }

    }

    public bool Add_to_Inv(Item item) // Метод для добавления предмета в инвентарь
    {
        int state = -1;
        if (item is CollectableItem) // В зависимости от разновидности, предметы помещаются  инвентарь по-разному
        {
            if (item is OneTimeItem)
            {
                if (item is PlacebleItem)
                {
                    for (int i = 0; i < inv_items.Length; i++)
                    {
                        if (inv_items[i].item == item && ((PlacebleItemState)inv_items[i]).count < ((PlacebleItem)item).max_count)
                        {
                            ((PlacebleItemState)inv_items[i]).count++;
                            Inv_changed?.Invoke(); //делегат, отвечающий за перерисовку инвентаря на UI
                            return true;
                        }
                    }
                    if (state == -1)
                    {
                        for (int i = 0; i < inv_items.Length; i++)
                        {
                            if (inv_items[i].item == null)
                            {
                                state = i;
                                break;
                            }
                        }
                    }
                    if (state == -1)
                    {
                        print("Inv zapolnen");
                        return false;
                    }
                    else
                    {
                        PlacebleItemState collstate = ScriptableObject.CreateInstance<PlacebleItemState>();
                        collstate.item = item;
                        collstate.count++;
                        inv_items[state] = collstate;
                        Inv_changed?.Invoke();
                        return true;
                    }
                }
                else
                {
                    for (int i = 0; i < inv_items.Length; i++)
                    {
                        if (inv_items[i].item == item && ((OneTimeItemState)inv_items[i]).count < ((OneTimeItem)item).max_count)
                        {
                            ((OneTimeItemState)inv_items[i]).count++;
                            Inv_changed?.Invoke();
                            return true;
                        }
                    }
                    if (state == -1)
                    {
                        for (int i = 0; i < inv_items.Length; i++)
                        {
                            if (inv_items[i].item == null)
                            {
                                state = i;
                                break;
                            }
                        }
                    }
                    if (state == -1)
                    {
                        print("Inv zapolnen");
                        return false;
                    }
                    else
                    {
                        OneTimeItemState collstate = ScriptableObject.CreateInstance<OneTimeItemState>();
                        collstate.item = item;
                        collstate.count++;
                        inv_items[state] = collstate;
                        Inv_changed?.Invoke();
                        return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < inv_items.Length; i++)
                {
                    if (inv_items[i].item == item && ((CollectableItemState)inv_items[i]).count < ((CollectableItem)item).max_count)
                    {
                        ((CollectableItemState)inv_items[i]).count++;
                        Inv_changed?.Invoke();
                        return true;
                    }
                }
                if (state == -1)
                {
                    for (int i = 0; i < inv_items.Length; i++)
                    {
                        if (inv_items[i].item == null)
                        {
                            state = i;
                            break;
                        }
                    }
                }
                if (state == -1)
                {
                    print("Inv zapolnen");
                    return false;
                }
                else
                {
                    CollectableItemState collstate = ScriptableObject.CreateInstance<CollectableItemState>();
                    collstate.item = item;
                    collstate.count++;
                    inv_items[state] = collstate;
                    Inv_changed?.Invoke();
                    return true;
                }
            }
        }
        else if (item is UsableItem)
        {
            for (int i = 0; i < inv_items.Length; i++)
            {
                if (inv_items[i].item == null)
                {
                    state = i;
                    break;
                }
            }
            if (state != -1)
            {
                UsableItemState usa = ScriptableObject.CreateInstance<UsableItemState>();
                usa.item = item;
                usa.Dur_now = ((UsableItem)item).Durability;
                inv_items[state] = usa;
                Inv_changed?.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void SwitchHand()
    {
        if (!b_system.building)
        {
            index_hand -= Input.GetAxis("Mouse ScrollWheel") * 5;
            if (index_hand < 0)
            {
                index_hand = 7;
            }
            else if (index_hand > 7)
            {
                index_hand = 0;
            }
            inhand = inv_items[(int)index_hand];
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                inhand = inv_items[0];
                index_hand = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                inhand = inv_items[1];
                index_hand = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                inhand = inv_items[2];
                index_hand = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                inhand = inv_items[3];
                index_hand = 3;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                inhand = inv_items[4];
                index_hand = 4;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                inhand = inv_items[5];
                index_hand = 5;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                inhand = inv_items[6];
                index_hand = 6;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                inhand = inv_items[8];
                index_hand = 8;
            }
            if (inhand.item != null)
            {
                hand_sprite.sprite = inhand.item.Icon;
            }
            else
            {
                hand_sprite.sprite = null;
            }
        }
    }
    public void DestroyTool()
    {
        if (inhand is UsableItemState tool && tool.Dur_now <= 1)
        {
            for (int i = 0; i < inv_items.Length; i++)
            {
                if (inv_items[i] == inhand)
                {
                    inv_items[i] = ScriptableObject.CreateInstance<ItemState>();
                    inhand = inv_items[i];
                    hand_sprite.sprite = null;
                    Inv_changed?.Invoke();
                }
            }
        }
    }
    public void ConsumeHandItem()
    {
        if (inhand is CollectableItemState hand)
        {
            if (hand.count > 1)
            {
                hand.count--;
                Inv_changed?.Invoke();
            }
            else if (hand.count == 1)
            {
                for (int i = 0; i < inv_items.Length; i++)
                {
                    if (inv_items[i] == inhand)
                    {
                        inv_items[i] = ScriptableObject.CreateInstance<ItemState>();
                        inhand = inv_items[i];
                        hand_sprite.sprite = null;
                        Inv_changed?.Invoke();
                    }
                }
            }
        }
    }

    public void ConsumeItem(ItemState its)
    {
        if (its is CollectableItemState cits)
        {
            if (cits.count > 1)
            {
                cits.count--;
                Inv_changed?.Invoke();
            }
            else if (cits.count == 1)
            {
                for (int i = 0; i < inv_items.Length; i++)
                {
                    if (inv_items[i] == its)
                    {
                        inv_items[i] = ScriptableObject.CreateInstance<ItemState>();
                        Inv_changed?.Invoke();
                    }
                }
            }
        }
    }
}
