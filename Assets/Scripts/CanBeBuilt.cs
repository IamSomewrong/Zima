using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBeBuilt : MonoBehaviour
{
    public bool canBeBuilt = true;
    SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 0, -90));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, 0, 90));
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        canBeBuilt = false;
        sprite.color = Color.red;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canBeBuilt = true;
        sprite.color = Color.white;
    }
}
