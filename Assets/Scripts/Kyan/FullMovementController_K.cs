using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class FullMovementController_K : MonoBehaviour
{

    #region variables

    #region References

    Rigidbody2D RB;
    SpriteRenderer SR;
    CapsuleCollider2D Col;

    #endregion

    #region Ground+Wall Check References

    Transform WallCheck1;
    Transform WallCheck2;
    Transform WallCheck3;
    Transform GroundCheck1;
    Transform GroundCheck2;
    Transform GroundCheck3;

    #endregion

    #region Movement

    [Header("MoveSpeed")]
    public float DefaultMoveSpeed;
    bool Moving;
    float HorizontalVelocity;
    public bool canmove = true;

    #endregion

    #region Jumping

    [Header("JumpSettings")]
    public float JumpForce;
    bool JumpDelay = false;
    [HideInInspector]
    public bool CanJump = true;

    #endregion

    #region WallJumping

    [Header("WallJumping")]
    public bool WallJumpOn;
    public int maxWallJumpCombo;
    int currentWallJumpCombo;

    #endregion

    #region Sliding

    [Header("Sliding")]
    public bool SlidingOn;
    public float SlideForce;
    public float SlideTime;
    bool isSlide = false;
    float CurrentSlideTime;

    #endregion

    #region Dashing

    [Header("Dashing")]
    public bool DashingOn;
    public float DashForce;
    public float DashDelay;
    [HideInInspector]
    public bool CanDash = true;

    #endregion

    #region Ground + wall Checks (layers)

    [Header("Layers")]
    public LayerMask GroundLayer;
    public LayerMask WallLayer;
    public bool isGrounded = true;
    public bool TouchingWall = true;

    #endregion

    #region Direction

    bool Left, Right, Up, Down = false;
    bool CanChangeWayfacing = true;

    #endregion

    #region sprites

    [System.Serializable]
    public class CustomSpritesClass
    {
        public Sprite Walk;
        public Sprite Jump;
        public Sprite Dash;
        public Sprite Crouch;
        public Sprite Slide;
    }

    [Header("Inputs")]
    public CustomSpritesClass Sprites;

    #endregion

    #region KeyInputs

    [System.Serializable]
    public class customKeyClass
    {
        public KeyCode LeftKey;
        public KeyCode RightKey;
        public KeyCode UpKey;
        public KeyCode DownKey;
        public KeyCode Jumpkey;
        public KeyCode CrouchKey;
        public KeyCode DashKey;
        public KeyCode InteractKey;
    }

    [Header("Inputs")]
    public customKeyClass KeyInputs;

    #endregion

    #endregion

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        Col = GetComponent<CapsuleCollider2D>();

        GroundCheck1 = transform.Find("GroundCheck1");
        GroundCheck2 = transform.Find("GroundCheck2");
        GroundCheck3 = transform.Find("GroundCheck3");

        WallCheck1 = transform.Find("WallCheck1");
        WallCheck2 = transform.Find("WallCheck2");
        WallCheck3 = transform.Find("WallCheck3");
    }

    private void Update()
    {
        GetInput();
        CheckGround();

        if(Moving == true)
        {
            GetComponent<Animator>().SetBool("Isrunning", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Isrunning", false);
        }
    }

    private void FixedUpdate()
    {
        moveCharacter();
    }

    void SwitchSide()
    {
        if (Left)
        {
            transform.localScale = new Vector3(-0.8f, transform.localScale.y, transform.localScale.z);
        }
        if (Right)
        {
            transform.localScale = new Vector3(0.8f , transform.localScale.y, transform.localScale.z);
        }
    }

    void decideWayFacing()
    {
        if (Input.GetKeyDown(KeyInputs.LeftKey))
        {
            Left = true;
            Right = false;
            SwitchSide();
        }
        if (Input.GetKeyDown(KeyInputs.RightKey))
        {
            Right = true;
            Left = false;
            SwitchSide();
        }

        if (Input.GetKeyDown(KeyInputs.UpKey))
        {
            Up = true;
        }
        if (Input.GetKeyDown(KeyInputs.DownKey) && !Up)
        {
            Down = true;
        }
        if (Input.GetKeyUp(KeyInputs.UpKey))
        {
            Up = false;
        }
        if (Input.GetKeyUp(KeyInputs.DownKey))
        {
            Down = false;
        }
    }

    void moveCharacter()
    {
        if (canmove)
        {
            RB.velocity = new Vector2(HorizontalVelocity * (DefaultMoveSpeed *100)* Time.fixedDeltaTime, RB.velocity.y);
        }

        if (Left && Input.GetKey(KeyInputs.LeftKey))
        {
            Moving = true;
            if(HorizontalVelocity > -1)
            {
                HorizontalVelocity -= Time.fixedDeltaTime * 10;
            }
            else
            {
                HorizontalVelocity = -1;
            }
        }
        else if (Right && Input.GetKey(KeyInputs.RightKey))
        {
            Moving = true;
            if (HorizontalVelocity < 1)
            {
                HorizontalVelocity += Time.fixedDeltaTime * 10;
            }
            else
            {
                HorizontalVelocity = 1;
            }
        }
        else
        {
            Moving = false;
            if (HorizontalVelocity < -0.1)
            {
                HorizontalVelocity += Time.fixedDeltaTime * 10;
            }
            if (HorizontalVelocity > 0.1)
            {
                HorizontalVelocity -= Time.fixedDeltaTime * 10;
            }
            if(HorizontalVelocity < 0.1 && HorizontalVelocity > -0.1)
            {
                HorizontalVelocity = 0;
            }
        }
        
    }

    void CheckGround()
    {
        if(Physics2D.OverlapCircle(GroundCheck1.position, 0.01f, GroundLayer) ||
           Physics2D.OverlapCircle(GroundCheck2.position, 0.01f, GroundLayer) ||
           Physics2D.OverlapCircle(GroundCheck3.position, 0.01f, GroundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if(Physics2D.OverlapCircle(WallCheck1.position, 0.01f, WallLayer)||
           Physics2D.OverlapCircle(WallCheck1.position, 0.01f, GroundLayer)||
           Physics2D.OverlapCircle(WallCheck2.position, 0.01f, WallLayer) ||
           Physics2D.OverlapCircle(WallCheck2.position, 0.01f, GroundLayer) ||
           Physics2D.OverlapCircle(WallCheck3.position, 0.01f, WallLayer) ||
           Physics2D.OverlapCircle(WallCheck3.position, 0.01f, GroundLayer))
        {
            TouchingWall = true;
        }
        else
        {
            TouchingWall = false;
        }

        if (isGrounded && !CanJump && !JumpDelay)
        {
            CanJump = true;
            GetComponent<Animator>().SetBool("IsJump", false);
        }
    }

    void GetInput()
    {
        if (CanChangeWayfacing)
        {
            decideWayFacing();
        }

        if (currentWallJumpCombo != 0 && isGrounded)
        {
            currentWallJumpCombo = 0;
        }

        CheckJump();

        CheckDash();

        CheckSlide();
    }


    void CheckJump()
    {

        if (!isGrounded && !TouchingWall)
        {
            CanJump = false;
        }
        else if (!CanJump && TouchingWall && currentWallJumpCombo < maxWallJumpCombo)
        {
            CanJump = true;
        }

        if (Input.GetKeyDown(KeyInputs.Jumpkey) && CanJump )
        {
            GetComponent<Animator>().SetBool("IsJump", false);

            GetComponent<Animator>().SetBool("IsJump", true);

            RB.velocity = new Vector2(0, 0);

            if (TouchingWall)
            {
                currentWallJumpCombo++;
            }

            JumpDelay = true;
            CanJump = false;
            RB.velocity += new Vector2(0, JumpForce);
            SR.sprite = Sprites.Jump;
            StartCoroutine(JumpDelayTime());

            IEnumerator JumpDelayTime()
            {
                yield return new WaitForSeconds(0.2f);
                JumpDelay = false;
            }
        }
    }

    void CheckDash()
    {
        if (Input.GetKeyDown(KeyInputs.DashKey) && CanDash)
        {
            GetComponent<Animator>().SetBool("IsDash", true);
            CanDash = false;
            canmove = false;

            float X = 0;
            float Y = 0;

            if (Right && Up) { X = 0.8f; }
            else if (Right && !Up) { X = 1.2f; }
            else if (Left && Up) { X = -0.8f; }
            else if (Left && !Up) { X = -1.2f; }
            if (Up) { Y = 1f; }
            else if (Down) { Y = -1.5f; }

            RB.velocity = new Vector2(0,0);
            RB.velocity += new Vector2(DashForce * X, DashForce * Y);

            StartCoroutine(DashDelayTime());

            IEnumerator DashDelayTime()
            {
                yield return new WaitForSeconds(DashDelay / 2);
                GetComponent<Animator>().SetBool("IsDash", false);
                canmove = true;
                yield return new WaitForSeconds(DashDelay);
                CanDash = true;
            }
        }
    }
    
    float velocityX;

    void CheckSlide()
    {
        if (Input.GetKeyDown(KeyInputs.CrouchKey) && isGrounded && !isSlide)
        {
            if (Mathf.Abs(RB.velocity.x) > 6)
            {
                canmove = false;
                velocityX = RB.velocity.x;
                RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y*4);
                transform.localPosition -= new Vector3(0,0.8f,0);
                Col.direction = CapsuleDirection2D.Horizontal;

                isSlide = true;

                GetComponent<Animator>().SetBool("IsSlide", true);
                CanChangeWayfacing = false;
                CurrentSlideTime = SlideTime;
            }
        }



        if (isSlide)
        {
            if (Input.GetKey(KeyInputs.CrouchKey) && CurrentSlideTime > 0)
            {
                CurrentSlideTime -= Time.deltaTime;

                if (Right && CurrentSlideTime > 0)
                {
                    RB.velocity = new Vector2(velocityX * SlideForce * CurrentSlideTime, RB.velocity.y);
                }
                else if (Left && CurrentSlideTime > 0)
                {
                    RB.velocity = new Vector2(velocityX * SlideForce * CurrentSlideTime, RB.velocity.y);
                }
            }
            else
            {
                transform.localPosition += new Vector3(0, 0.6f, 0);
                Col.direction = CapsuleDirection2D.Vertical;
                GetComponent<Animator>().SetBool("IsSlide", false);
                CanChangeWayfacing = true;
                canmove = true;
                isSlide = false;
            }
        }
    }

}
