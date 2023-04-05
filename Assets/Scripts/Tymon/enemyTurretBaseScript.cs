using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTurretBaseScript : MonoBehaviour
{
    public float speedOfRotation = 1, directionOfRotation = 1, maxRotationLeft, maxRotationRight;
    private float cooldownTimeBetweenShots = 2F, timer;
    public Transform rotatingPoint, zRotation, playerTransform, firePoint;
    private Vector3 zAxis = new Vector3(0, 0, 1);
    public GameObject raycastStartPoint, player, bulletPrefab;
    public bool seesPlayer = false, readyForFire = false, turretAggroLoss = false;
    Vector3 raycastDirection;
    // Start is called before the first frame update
    void Start()
    {
        zAxis.z += 360;
        maxRotationLeft += 360;
        maxRotationRight += 360;
        zRotation = gameObject.GetComponent<Transform>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        Vector3 locationOfPlayer = player.transform.position - transform.position;
        
        raycastDirection = gameObject.transform.eulerAngles;
        RaycastHit2D enemyVision = Physics2D.Raycast(raycastStartPoint.transform.position, transform.TransformDirection(Vector2.right));
        Debug.DrawRay(raycastStartPoint.transform.position, transform.TransformDirection(Vector2.right) * enemyVision.distance, Color.green);
        Debug.Log(enemyVision.collider.gameObject.name);


        if (transform.eulerAngles.z >= maxRotationLeft || transform.eulerAngles.z <= maxRotationRight)
        {
            directionOfRotation = directionOfRotation * -1;

        }
        if(timer > cooldownTimeBetweenShots)
        {
            timer = timer - cooldownTimeBetweenShots;
            readyForFire = true;
        }
        while (enemyVision.collider.gameObject == player && readyForFire)
        {
            readyForFire = false;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        if (enemyVision.collider.gameObject == player)
        {
            StopAllCoroutines();
            Debug.Log("");
            seesPlayer = true;
            turretAggroLoss = true;
            Debug.Log(seesPlayer);


        }
        if(seesPlayer && enemyVision.collider.gameObject != player)
        {
            StartCoroutine(LoseAggroTimer());
            

        }
        if (seesPlayer)
        {

            transform.right = player.transform.position - transform.position;
        }
        if (!seesPlayer)
        {
            transform.RotateAround(rotatingPoint.position, zAxis, speedOfRotation * directionOfRotation);
        }



    }
    IEnumerator LoseAggroTimer()
    {
        if (turretAggroLoss == true)
        {
            turretAggroLoss = false;
            Debug.Log("soup0");
            yield return new WaitForSeconds(5F);
            gameObject.transform.eulerAngles = new Vector3(0, 0, maxRotationRight - maxRotationLeft + 360);
            seesPlayer = false;
        }

    }
    /*IEnumerator IFireTheBullets() {
        while (true)
        {
            yield return new WaitForSeconds(2F);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }



    }*/
}
