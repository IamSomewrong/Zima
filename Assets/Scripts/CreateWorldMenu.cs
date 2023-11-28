using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;

public class CreateWorldMenu : MonoBehaviour
{
    public GameObject worldItem;
    public void MakeMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        string json;
        using (StreamReader sr = new StreamReader("Worlds" + ".wl"))
        {
            json = sr.ReadToEnd();
        }
        if (json != "")
        {
            WorldList wl = JsonUtility.FromJson<WorldList>(json);
            gameObject.GetComponent<RectTransform>().rect.Set(0, 0, 0, 125 * wl.names.Count);
            for (int i = 0; i < wl.names.Count; i++)
            {
                GameObject item = Instantiate(worldItem, gameObject.transform);
                item.transform.GetChild(1).GetComponent<TMP_Text>().text = wl.names[i];
            }
        }
    }
}
