using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject door;

    Vector3 startpos;
    bool up = true;
    public Vector3 movement;
    public float speed;

    private void Start()
    {
        startpos = transform.position;
    }

    private void Update()
    {
        if(transform.position.y < startpos.y + movement.y && up)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        else if(transform.position.y >= startpos.y + movement.y && up)
        {
            up = false;
        }

        if (transform.position.y > startpos.y - movement.y && !up)
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
        else if (transform.position.y <= startpos.y - movement.y && !up)
        {
            up = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(door);
            Destroy(gameObject);
        }
    }
}
