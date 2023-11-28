using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject to_move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (to_move != null)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(to_move.transform.position.x, to_move.transform.position.y, gameObject.transform.position.z), Time.deltaTime);
        }
    }
}
