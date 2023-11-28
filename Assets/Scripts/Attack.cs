using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int distance;
    public float reload_time;
    bool Reloaded;
    public TakeItems inv;
    Animator _animator;

    void Start()
    {
        Reloaded = true;
        inv = GetComponent<TakeItems>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(inv.inhand.item is UsableItem tool && tool.ForWood)
        {
            if (Input.GetMouseButton(0))
            {
                if (Reloaded)
                {
                    _animator.SetBool("Attacking", true);
                    RaycastHit2D[] r = Physics2D.RaycastAll(gameObject.transform.position, Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position), distance);
                    for (int i = 0; i < r.Length; i++)
                    {
                        if (r[i].collider.gameObject != gameObject && r[i].collider.gameObject.tag == "Entity")
                        {
                            ((Die)r[i].collider.gameObject.GetComponent("Die")).Damage();
                            (inv.inhand as UsableItemState).Dur_now--;
                        }
                    }
                    Reloaded = false;
                    StartCoroutine(Reload());
                    
                }
            }
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reload_time);
        Reloaded = true;
        _animator.SetBool("Attacking", false);
    }
}
