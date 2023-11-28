using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuiltProject : MonoBehaviour
{
    enum Clip_Dir
    {
        None,
        Top,
        Bot,
        Right,
        Left
    }

    Clip_Dir clip_Dir = Clip_Dir.None;
    GameObject clip_obj;
    public GameObject obj_to_build;
    CanBeBuilt canBeBuilt;
    public static UnityAction<GameObject, Vector2> Builded;

    private void Start()
    {
        canBeBuilt = GetComponentInChildren<CanBeBuilt>();
    }
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(pos.x, pos.y, 1);
        if(clip_Dir == Clip_Dir.Top)
        {
            transform.GetChild(0).position = new Vector3(clip_obj.transform.position.x, clip_obj.transform.position.y + 1, 1);
        }
        else if (clip_Dir == Clip_Dir.Bot)
        {
            transform.GetChild(0).position = new Vector3(clip_obj.transform.position.x, clip_obj.transform.position.y - 1, 1);
        }
        else if (clip_Dir == Clip_Dir.Right)
        {
            transform.GetChild(0).position = new Vector3(clip_obj.transform.position.x + 1, clip_obj.transform.position.y, 1);
        }
        else if (clip_Dir == Clip_Dir.Left)
        {
            transform.GetChild(0).position = new Vector3(clip_obj.transform.position.x - 1, clip_obj.transform.position.y, 1);
        }
        if (Input.GetMouseButtonDown(0) && canBeBuilt.canBeBuilt)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Builded?.Invoke(Instantiate(obj_to_build, transform.GetChild(0).position, transform.GetChild(0).rotation), new Vector2((int)(player.transform.position.x / 100), (int)(player.transform.position.y / 100)));
            player.GetComponent<Build>().Builded();
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("Trig");
        if (collision.CompareTag("Clips"))
        {
            clip_obj = collision.gameObject;
            if(Mathf.Abs(transform.position.x - collision.transform.position.x) < 0.5f && transform.position.y > collision.transform.position.y)
            {
                clip_Dir = Clip_Dir.Top;
            }
            else if (Mathf.Abs(transform.position.x - collision.transform.position.x) < 0.5f && transform.position.y < collision.transform.position.y)
            {
                clip_Dir = Clip_Dir.Bot;
            }
            else if(Mathf.Abs(transform.position.y - collision.transform.position.y) < 0.5f && transform.position.x > collision.transform.position.x)
            {
                clip_Dir = Clip_Dir.Right;
            }
            else if (Mathf.Abs(transform.position.y - collision.transform.position.y) < 0.5f && transform.position.x < collision.transform.position.x)
            {
                clip_Dir = Clip_Dir.Left;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Clips"))
        {
            clip_Dir = Clip_Dir.None;
            transform.GetChild(0).position = transform.position;
        }
    }
}
