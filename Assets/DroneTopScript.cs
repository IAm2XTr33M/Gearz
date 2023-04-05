using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTopScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(Move(0.01f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Move(-0.01f));
        }
    }

    IEnumerator Move(float Y)
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.position -= new Vector3(0, Y, 0);
        }
    }


}
