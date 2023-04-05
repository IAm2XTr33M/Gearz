using UnityEngine;

public class Player_Movement_T : MonoBehaviour
{
    Rigidbody2D _rb2d;
    public KeyCode leftKey, rightKey;
    public bool canJump = true;
    public float playerSpeed, playerJumpStrengh;
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirInput = 0;
        if (Input.GetKey(leftKey))
        {
            dirInput = -1;
        }
        else if (Input.GetKey(rightKey))
        {
            dirInput = 1;
        }
        transform.position += Vector3.right * dirInput * Time.deltaTime * playerSpeed;
        if (canJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            _rb2d.AddForce(playerJumpStrengh * Vector3.up, ForceMode2D.Impulse);
            //canJump = false;
        }
    }
}
