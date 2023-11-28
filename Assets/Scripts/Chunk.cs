using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    Vector2 pos;
    public List<GameObject> gameObjects;

    public Chunk(Vector2 pos)
    {
        this.pos = pos;
        gameObjects = new List<GameObject>();
    }

    public Vector2 Pos
    {
        get
        {
            return pos;
        }
    }
}
