using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float temperature = 36.6f;
    public float clothes_temp = 0;
    public float outside_temp = -30;
    public float speed_of_cooling = 1;
    public UnityAction<Vector2> GenChunk;
    public UnityAction<Vector2> EraseChunk;
    LvLGen gen;
    Rigidbody2D rb;
    Animator animator;
    void Start()
    {
        gen = Camera.main.GetComponent<LvLGen>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Camera.main.GetComponent<General>().Player = this;
        Camera.main.GetComponent<CameraMove>().to_move = gameObject;
        Camera.main.GetComponent<CameraMove>().enabled = true;
        GenChunk += Camera.main.GetComponent<General>().NewChunk;
        EraseChunk += Camera.main.GetComponent<General>().DeleteChunk;
        gameObject.transform.position = Camera.main.GetComponent<General>().playerPos;
        GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50)));
    }
    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(h, v, 0).normalized * speed;
        rb.velocity = move;
        if ((h > 0 && transform.localScale.x < 0) || (h < 0 && transform.localScale.x > 0))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }
        animator.SetFloat("Horizon", Mathf.Abs(h));
        animator.SetFloat("Vertical", v);
        if (gameObject.transform.position.x % 50 > 13 && gameObject.transform.position.x % 50 < 13.06)
        {
            if (h > 0)
            {
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50)));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50) - 1));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50) + 1));
            }
            else if (h < 0)
            {
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50) + 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50) - 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 2, (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 2, (int)(gameObject.transform.position.y / 50) + 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 2, (int)(gameObject.transform.position.y / 50) - 1));
            }
        }
        if (gameObject.transform.position.x % 50 < -13 && gameObject.transform.position.x % 50 > -13.06)
        {
            if (h < 0)
            {
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50)));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50) - 1));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50) + 1));
            }
            else if(h > 0)
            {
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50) + 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50) - 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 2, (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 2, (int)(gameObject.transform.position.y / 50) + 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 2, (int)(gameObject.transform.position.y / 50) - 1));
            }
        }
        if (gameObject.transform.position.y % 50 > 13 && gameObject.transform.position.y % 50 < 13.06)
        {
            if (v > 0)
            {
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.y / 50), (int)(gameObject.transform.position.y / 50) + 1));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.y / 50) + 1, (int)(gameObject.transform.position.y / 50) + 1));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.y / 50) - 1, (int)(gameObject.transform.position.y / 50) + 1));
            }
            else if (v < 0)
            {
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) + 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50) + 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50) + 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) + 2));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50) + 2));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50) + 2));
            }
            
        }
        if (gameObject.transform.position.y % 50 < -13 && gameObject.transform.position.y % 50 > -13.06)
        {
            if (v < 0)
            {
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.y / 50), (int)(gameObject.transform.position.y / 50) - 1));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.y / 50) + 1, (int)(gameObject.transform.position.y / 50) - 1));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.y / 50) - 1, (int)(gameObject.transform.position.y / 50) - 1));
            }
            else if (v > 0)
            {
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) - 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50) - 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50) - 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) - 2));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50) - 2));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50) - 2));
            }
        }

        if (gameObject.transform.position.x % 50 > 38 && gameObject.transform.position.x % 50 < 38.06)
        {
            if (h > 0)
            {
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) + 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) - 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50) + 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50) - 1));
            }
            else if (h < 0)
            {
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50)));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) - 1));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) + 1));
            }
        }
        if (gameObject.transform.position.x % 50 < -38 && gameObject.transform.position.x % 50 > -38.06)
        {
            if (h < 0)
            {
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) + 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) - 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50) + 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50) - 1));
            }
            else if (h > 0)
            {
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50)));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) - 1));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) + 1));
            }
        }
        if (gameObject.transform.position.y % 50 > 38 && gameObject.transform.position.y % 50 < 38.06)
        {
            if (v > 0)
            {
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) - 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50) - 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50) - 1));
            }
            else if (v < 0)
            {
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.y / 50), (int)(gameObject.transform.position.y / 50)));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.y / 50) + 1, (int)(gameObject.transform.position.y / 50)));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.y / 50) - 1, (int)(gameObject.transform.position.y / 50)));
            }
        }
        if (gameObject.transform.position.y % 50 < -38 && gameObject.transform.position.y % 50 > -38.06)
        {
            if (v < 0)
            {
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50)));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50), (int)(gameObject.transform.position.y / 50) + 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) + 1, (int)(gameObject.transform.position.y / 50) + 1));
                EraseChunk?.Invoke(new Vector2((int)(gameObject.transform.position.x / 50) - 1, (int)(gameObject.transform.position.y / 50) + 1));
            }
            else if (v > 0)
            {
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.y / 50), (int)(gameObject.transform.position.y / 50)));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.y / 50) + 1, (int)(gameObject.transform.position.y / 50)));
                GenChunk?.Invoke(new Vector2((int)(gameObject.transform.position.y / 50) - 1, (int)(gameObject.transform.position.y / 50)));
            }
        }

        //outside_temp = Mathf.PerlinNoise((transform.position.x + gen.offset.x) / (gen.zoom * 2) + gen.off.x, (transform.position.y + gen.offset.y) / (gen.zoom * 2) + gen.off.y) * gen.intensivity * 30 - 45;
        //Freeze();
    }

    //private void Freeze()
    //{
    //    float cold_speed = (clothes_temp + outside_temp) / 36.6f / speed_of_cooling;
    //    if(cold_speed > 0 && temperature < 36.6)
    //    {
    //        temperature += cold_speed;
    //    }
    //    else if(cold_speed < 0 && temperature > 32)
    //    {
    //        temperature += cold_speed;
    //    }
    //}
}
