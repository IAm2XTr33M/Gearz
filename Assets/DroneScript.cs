using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour
{
    public Vector3 Move;
    public float Speed;
    float realSpeed;

    Vector3 startPos;
    Vector3 endPos;
    Vector3 posToGo;

    bool moveHorizontal = false;

    private void Start()
    {
        realSpeed = Speed;

        if (Move.x != 0)
        {
            moveHorizontal = true;
        }

        startPos = transform.position;
        endPos = transform.position + Move;
        posToGo = endPos;
    }

    private void Update()
    {
        if (moveHorizontal)
        {
            if(Mathf.Abs(transform.position.x - posToGo.x) < 0.1f)
            {
                SwapSpeed();
            }
            transform.position += new Vector3(realSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            if (Mathf.Abs(transform.position.y - posToGo.y) < 0.1f)
            {
                SwapSpeed();
            }
            transform.position += new Vector3(0, realSpeed * Time.deltaTime, 0);
        }

        void SwapSpeed()
        {
            if(posToGo == startPos)
            {
                posToGo = endPos;
                realSpeed = Speed;
            }
            else
            {
                posToGo = startPos;
                realSpeed = -Speed;
            }
        }
    }
}
