using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWorld : MonoBehaviour
{
    General gen;
    private void Start()
    {
        gen = Camera.main.GetComponent<General>();
    }

    public void Create()
    {
        gen.CreateWorld();
    }

    public void Name(string str)
    {
        gen.saveName = str;
    }

    public void Seed(string str)
    {
        gen.seed = str;
    }
}
