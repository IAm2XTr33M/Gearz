using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer_K : MonoBehaviour
{
    public Transform Player;
    public GameObject CameraObject;

    public KeyCode DownKey;

    public Vector2 PlayerMoveSize;
    public Vector2 PlayerMoveOffset;

    public float MoveSpeed;
    public float LowestYpos;

    List<Vector3> StoredPositions = new List<Vector3>();

    private void FixedUpdate()
    {
        float minX = transform.position.x - PlayerMoveSize.x / 2 + PlayerMoveOffset.x;
        float maxX = transform.position.x + PlayerMoveSize.x / 2 + PlayerMoveOffset.x;

        float minY = transform.position.y - PlayerMoveSize.y / 2 + PlayerMoveOffset.y;
        float maxY = transform.position.y + PlayerMoveSize.y / 2 + PlayerMoveOffset.y;

        StoredPositions.Add(Player.position);

        if (Player.position.x < minX || Player.position.x > maxX)
        {
            transform.position += new Vector3(StoredPositions[1].x - StoredPositions[0].x, 0);
        }
        if (Player.position.y < minY || Player.position.y > maxY)
        {
            transform.position += new Vector3(0, StoredPositions[1].y - StoredPositions[0].y);
        }



        if (Input.GetKey(DownKey) && CameraObject.transform.localPosition.y > LowestYpos)
        {
            CameraObject.transform.localPosition -= new Vector3(0,MoveSpeed * Time.deltaTime,0);
        }
        else if (!Input.GetKey(DownKey) && CameraObject.transform.localPosition.y < 0)
        {
            CameraObject.transform.localPosition += new Vector3(0, MoveSpeed * Time.deltaTime, 0);
        }

        StoredPositions.RemoveAt(0);
    }

    private void Start()
    {
        StoredPositions.Add(Player.position);
    }
}
