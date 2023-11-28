using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    bool showed = true;
    public List<GameObject> ui_els = new List<GameObject>();
    public GameObject Menu;
    General general;
    private void Start()
    {
        ui_els.Add(transform.GetChild(1).gameObject);
        ui_els.Add(transform.GetChild(2).gameObject);
        Menu = transform.GetChild(3).gameObject;
        general = Camera.main.GetComponent<General>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (showed)
            {
                foreach(GameObject go in ui_els)
                {
                    go.SetActive(false);
                }
                showed = false;
            }
            else if (!showed)
            {
                foreach (GameObject go in ui_els)
                {
                    go.SetActive(true);
                }
                showed = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Menu.activeSelf)
            {
                foreach (GameObject go in ui_els)
                {
                    go.SetActive(false);
                }
                showed = false;
                Menu.SetActive(true);
            }
            else
            {
                Menu.SetActive(false);
            }
            
        }
    }
    public void SaveWorld()
    {
        general.SaveWorld();
    }
}
