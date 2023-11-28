using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenu : MonoBehaviour
{
    General gen;

    private void Start()
    {
        gen = Camera.main.GetComponent<General>();
    }
    public void ToMenu()
    {
        gen.SaveWorld();
        SceneManager.LoadScene(1);
        Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
    }
}
