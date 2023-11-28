using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public bool building = false;
    public GameObject project;
    GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !building && Player.GetComponent<TakeItems>().inhand is PlacebleItemState obj)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject go = Instantiate(project, new Vector3(pos.x, pos.y, 1), Quaternion.identity);
            go.GetComponent<BuiltProject>().obj_to_build = ((PlacebleItem)obj.item).obj_to_place;
            go.GetComponentInChildren<SpriteRenderer>().sprite = ((PlacebleItem)obj.item).Icon;
            building = true;
        }
    }
    public void Builded()
    {
        building = false;
        Player.GetComponent<TakeItems>().ConsumeHandItem();
    }
}
