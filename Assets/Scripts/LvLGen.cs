using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LvLGen : MonoBehaviour
{
    public General general;
    public Vector2Int size;
    public float intensivity;
    public float zoom;
    public Vector2 offset;
    public Vector2Int off;
    public Vector2Int off2;
    public Vector2Int off3;
    public List<GameObject> objectsToSpawn;


    public Chunk MapGen(Vector2 ch_pos)
    {
        Chunk t_chunk = new Chunk(ch_pos);
        for (int x = -size.x + size.x * 2 * (int)ch_pos.x; x < size.x + size.x * 2 * (int)ch_pos.x; x++)
        {
            for (int y = -size.y + size.y * 2 * (int)ch_pos.y; y < size.y + size.y * 2 * (int)ch_pos.y; y++)
            {
                if(ch_pos.x < -10)
                {
                    GameObject t = objectsToSpawn[7];
                    t_chunk.gameObjects.Add(Instantiate(t, new Vector3(x, y, 0), Quaternion.identity));
                }
                else
                {
                    var gr = Mathf.PerlinNoise((x + offset.x) / (zoom * 2) + general.offs[0].x, (y + offset.y) / (zoom * 2) + general.offs[0].y) * intensivity;
                    var gr_river = Mathf.PerlinNoise((x + offset.x) / (zoom * 6) + general.offs[1].x, (y + offset.y) / (zoom * 6) + general.offs[1].y) * intensivity;
                    GameObject t = getColorByInt(gr);
                    t = getColorRiv(gr_river, t, gr);
                    t_chunk.gameObjects.Add(Instantiate(t, new Vector3(x, y, 0), Quaternion.identity));
                    if (ch_pos.x > 10)
                    {
                        if (Random.Range(0, 100) >= 90)
                        {
                            float rx = x + Random.Range(-0.5f, 0.5f);
                            float ry = y + Random.Range(-0.5f, 0.5f);
                            t_chunk.gameObjects.Add(Instantiate(objectsToSpawn[6], new Vector3(rx, ry, 0), Quaternion.identity));
                        }
                    }
                    else
                    {
                        var gr_forest = Mathf.PerlinNoise((x + offset.x) / (zoom * 2) + general.offs[2].x, (y + offset.y) / (zoom * 2) + general.offs[2].y) * intensivity;
                        var gr_forest2 = Mathf.PerlinNoise((x + offset.x) / (zoom * 2) + general.offs[2].x, (y + offset.y) / (zoom * 2) + general.offs[2].y) * intensivity;
                        if (Random.Range(0, 100) >= 50)
                        {
                            float rx = x + Random.Range(-0.5f, 0.5f);
                            float ry = y + Random.Range(-0.5f, 0.5f);
                            if (Spawn_Trees(gr_forest * gr_forest2, gr, gr_river, rx, ry))
                            {
                                t_chunk.gameObjects.Add(Instantiate(objectsToSpawn[0], new Vector3(rx, ry, 0), Quaternion.identity));
                            }
                        }

                    }
                }
                
            }
        }

        return t_chunk;
    }

    public GameObject getColorByInt(float inp)
    {

        //var outp = (Tile)Palette.GetComponentInChildren<Tilemap>().GetTile(new Vector3Int(2, 0, 0));
        var outp = objectsToSpawn[1];

        if (inp < Mathf.Infinity)
        {
            outp = objectsToSpawn[4];
        }
        if (inp < 0.5f)
        {
            outp = objectsToSpawn[3];
        }
        if (inp < 0.3f)
        {
            outp = objectsToSpawn[2];
        }
        if (inp < 0.2f)
        {
            outp = objectsToSpawn[1];
        }
        return outp;
    }
    public GameObject getColorRiv(float inp, GameObject inp_tile, float noise2)
    {
        GameObject outp = inp_tile;
        if ((inp >= 0.27f && inp < 0.29f || inp <= 0.18f && inp > 0.16f) && noise2 >= 0.5f)
        {
            outp = objectsToSpawn[3];
        }
        if ((inp >= 0.25f && inp < 0.27f || inp <= 0.2f && inp > 0.18f) && noise2 >= 0.3f)
        {
            outp = objectsToSpawn[2];
        }
        if (inp > 0.2f && inp < 0.25f)
        {
            outp = objectsToSpawn[1];
        }
        return outp;
    }
    public bool Spawn_Trees(float inp, float noise2, float noise3, float x, float y)
    {
        if(inp < 0.3f && noise2 > 0.2f && (noise3 >= 0.25f || noise3 <= 0.2f))
        {
            return true;
        }
        return false;
    }
    public bool Spawn_Mountains()
    {
        return true;
    }
    void Start()
    {
        objectsToSpawn = general.GOsToLoad;
        off = new Vector2Int(Random.Range(-32767, 32767), Random.Range(-32767, 32767));
        off2 = new Vector2Int(Random.Range(-32767, 32767), Random.Range(-32767, 32767));
        off3 = new Vector2Int(Random.Range(-32767, 32767), Random.Range(-32767, 32767));
    }
}
