using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFloor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (FindObjectOfType<GearScript>().canDie)
            {
                FindObjectOfType<GearScript>().LoseOneGear();
            }
        }
    }
}
