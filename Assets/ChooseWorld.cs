using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore;

public class ChooseWorld : MonoBehaviour
{
    public void Load()
    {
        Camera.main.GetComponent<General>().saveName = transform.GetChild(1).GetComponent<TMP_Text>().text;
        SceneManager.LoadScene(2);
    }
}
