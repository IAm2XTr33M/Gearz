using System.Collections;
using UnityEngine;
public class Enemy_Base_Script_T : MonoBehaviour
{
    // Start is called before the first frame update
    public float enemyDamageAmount, amountOfHealth;
    public bool seesPlayer = false, rotationEnabled, withinAttackRange = false, wallMovement = false;
    public float direction = 1, movementSpeed = 10, leftMovementLimit, rightMovementLimit, directionToPlayer, stopMoving, enemyDamageController, raycastMaxDistance = 20;
    public GameObject player, raycastSpawnPoint;
    public Vector3 xDifferenceBetweenPlayer;
    public Vector3 rotation = new Vector3(0, 180, 0);
    public Rigidbody2D rgb2d; 
    void Start()
    {
        enemyDamageController = enemyDamageAmount;
        enemyDamageAmount = 0;
        rgb2d = gameObject.GetComponent<Rigidbody2D>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            seesPlayer = true;
            wallMovement = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = direction * -1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D enemyVision = Physics2D.Raycast(raycastSpawnPoint.transform.position, Vector2.right * direction, raycastMaxDistance);
        Debug.DrawRay(raycastSpawnPoint.transform.position, Vector2.right * enemyVision.distance * direction, Color.green);
        if (enemyVision.collider.gameObject == player)
        {
            seesPlayer = true;
            wallMovement = true;
        }
        if (Mathf.Abs(xDifferenceBetweenPlayer.x) < 0.5)
        {
            rotationEnabled = true;
            stopMoving = 1;

        }
        if (xDifferenceBetweenPlayer.x > 2 || xDifferenceBetweenPlayer.x < -2)
        {
            rotationEnabled = true;
            stopMoving = 1;
        }
        if (xDifferenceBetweenPlayer.x > 25 || xDifferenceBetweenPlayer.x < -25 || xDifferenceBetweenPlayer.y > 15 || xDifferenceBetweenPlayer.y < -10)
        {
            seesPlayer = false;
        }
        if ( xDifferenceBetweenPlayer.x > 5 || xDifferenceBetweenPlayer.x < -5)
        {
            withinAttackRange = false;
            StopCoroutine(EnemyAttack());
        }
        if (seesPlayer == true && xDifferenceBetweenPlayer.x < 2.5 && withinAttackRange == false && xDifferenceBetweenPlayer.y < 5|| seesPlayer == true && xDifferenceBetweenPlayer.x > -2.5 && withinAttackRange == false && xDifferenceBetweenPlayer.y < 5)
        {
            withinAttackRange = true;
            GetComponent<Animator>().SetBool("Attacking", true);
            StartCoroutine(EnemyAttack());
        }
        xDifferenceBetweenPlayer = gameObject.transform.position - player.transform.position;
        if(xDifferenceBetweenPlayer.x > 0)
        {
            directionToPlayer = -1;
        }
        else
        {
            directionToPlayer = 1;
        }
        if(wallMovement == false)
        {
            if(gameObject.transform.position.x <= leftMovementLimit)
            {
                direction = direction * -1;
            }
            if (gameObject.transform.position.x >= rightMovementLimit)
            {
                direction = direction * -1;
            }
        }

        if (seesPlayer == false)
        {
            
            transform.position += Vector3.right * direction * Time.deltaTime * movementSpeed * stopMoving;
        }
        else
        {
            transform.position += Vector3.right * directionToPlayer * Time.deltaTime * movementSpeed * stopMoving;
            if (xDifferenceBetweenPlayer.x < 0.5 || xDifferenceBetweenPlayer.x > -0.5)
            {
                rotationEnabled = false;
                stopMoving = 0;
            }
        }
        if(rotationEnabled == true)
        {
            if(direction == 1)
            {
                rotation.y = 180;
            }else if (direction == -1)
            {
                rotation.y = 0;
            }
            transform.eulerAngles = rotation;
        }

        if(amountOfHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator EnemyAttack()
    {
        while (true)
        {
            enemyDamageAmount = enemyDamageController;
            yield return new WaitForSeconds(1F);
            enemyDamageAmount = 0;
            GetComponent<Animator>().SetBool("Attacking", false);
            yield return new WaitForSeconds(0.6F);

        }
    }
}
