using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_Door_K : MonoBehaviour
{
    public FullMovementController_K controller;

    public GameObject HitBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!controller.CanDash)
        {
            BreakDoor();
        }
    }

    void BreakDoor()
    {
        Destroy(HitBox);
        GetComponent<SpriteRenderer>().color = new Color(0.5188679f, 0.5188679f, 0.5188679f);
    }
}
