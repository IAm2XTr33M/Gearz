using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundMove : MonoBehaviour
{
    public float speed;

    float moveX;
    float moveY;

    Vector3 startpos;
    void Start()
    {
        startpos = transform.position;

        moveX = Random.Range(-speed, speed);
        moveY = Random.Range(-speed, speed);
    }

    //0.9f

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "StartScene")
        {
            SceneManager.LoadScene("Main");
        }

        if(Random.Range(0,200) > 198)
        {
            moveX = Random.Range(-speed, speed);
        }
        if (Random.Range(0, 200) > 198)
        {
            moveY = Random.Range(-speed, speed);
        }

        if(Mathf.Abs(transform.position.x) < startpos.x + 0.9f)
        {
            transform.position += new Vector3(moveX * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position -= new Vector3(moveX * Time.deltaTime, 0, 0);
        }

        if (Mathf.Abs(transform.position.y) < startpos.y + 0.9f)
        {
            transform.position += new Vector3(0,moveY * Time.deltaTime, 0);
        }
        else
        {
            transform.position -= new Vector3(0, moveY * Time.deltaTime, 0);

        }
    }
}
