using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Die : MonoBehaviour
{
    public int hp;
    public GameObject drop;
    public Item item_drop;
    public static UnityAction<GameObject, Vector2> Dead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            GameObject droped = Instantiate(drop, transform.position, Quaternion.identity);
            droped.GetComponent<Take>().item = item_drop;
            droped.GetComponent<SpriteRenderer>().sprite = item_drop.Icon;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Dead?.Invoke(gameObject, new Vector2((int)(player.transform.position.x / 50), (int)(player.transform.position.y / 50)));
            GameObject.Destroy(gameObject);
        }
    }
    public void Damage()
    {
        hp--;
        gameObject.GetComponent<Animation>().Play("Damage_Tree");
    }
}
