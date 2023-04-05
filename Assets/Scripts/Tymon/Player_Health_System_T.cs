using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player_Health_System_T : MonoBehaviour
{
    public GameObject enemies;
    public GameObject enemiesParent;
    public GameObject currentEnemies;

    public GameObject repairStationObj, respawnObject, black, energyStationObject;
    public Vector3 respawnObjectPosition;
    public bool atRechargeBattery = false, holding = false, atRepairStation = false, immunityFramesCooldownCheck = false;
    public KeyCode interact;
    public float playerHealth = 5, playerLives = 4 , electricityLevel = 100, finalTime;
    float refillAmount;
    float holdTime;
    BoxCollider2D boxc;

    DeathScreen deathScreen;

    // Start is called before the first frame update
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && immunityFramesCooldownCheck == false)
        {
            playerHealth -= collision.gameObject.GetComponent<Enemy_Base_Script_T>().enemyDamageAmount;
            StartCoroutine(ImmunityFramesCooldown());
            immunityFramesCooldownCheck = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag ("Enemy"))
        {

            playerHealth -= collision.gameObject.GetComponent<Enemy_Base_Script_T>().enemyDamageAmount;

        }
        if(atRechargeBattery == false && collision.CompareTag("Station") && collision.gameObject.GetComponent<Station_Controller>().EnergyLeft > 0 ) {
            atRechargeBattery = true;
            refillAmount = collision.gameObject.GetComponent<Station_Controller>().EnergyLeft;
            energyStationObject = collision.gameObject;


        }
        if(collision.CompareTag("TurretProjectile"))
        {
            playerHealth -= 5;
        }
        if(collision.tag == "RepairStation" && atRepairStation == false)
        {
            atRepairStation = true;
            repairStationObj = collision.gameObject;
        }
        if(collision.name == "EndGoal")
        {
            ScoreAndTimeController.TotalTime();
            SceneManager.LoadScene("EndScene");
        }
        if (collision.CompareTag("RespawnPlace"))
        {
            respawnObject = gameObject;
            respawnObjectPosition = respawnObject.transform.position;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        atRechargeBattery = false;
        atRepairStation = false;

    }
    void Start()
    {
        //currentEnemies = Instantiate(enemies, transform.parent = enemiesParent.transform);
        deathScreen = FindObjectOfType<DeathScreen>();
        boxc = GetComponent<BoxCollider2D>();
        StartCoroutine(RemainingElectricity());
        respawnObjectPosition = gameObject.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if(atRepairStation == true && Input.GetKeyDown(KeyCode.E))
        {
            playerHealth = 50;
        }
        if (Input.GetKeyDown(KeyCode.E) && atRechargeBattery == true){
            holdTime = Time.time;
            holding = true;
            
        }
        if ((Time.time - holdTime) >= 2 && refillAmount != 0 && holding == true)
        {
            electricityLevel = 100;
            refillAmount -= 1;
            energyStationObject.GetComponent<Station_Controller>().EnergyLeft = 0;
        }            
        if (Input.GetKeyUp(KeyCode.E))
        {
            holding = false;
            //StopCoroutine(HoldInteract());
        }

        if(electricityLevel <= 0)
        {
            SceneManager.LoadScene("Main");
        }
        //Debug.Log(atRechargeBattery);
        //Debug.Log("Current health is " + playerHealth);
        //Debug.Log("Remaining power is " + electricityLevel);
    }
    IEnumerator RemainingElectricity()
    {
        while (true)
        {
            electricityLevel -= 1;
            yield return new WaitForSeconds(1F);
        }



    }
    IEnumerator HoldInteract()
    {
            Debug.Log("jes");
            yield return new WaitForSeconds(2F);
            electricityLevel = 100;
    }
    IEnumerator ImmunityFramesCooldown()
    {
        yield return new WaitForSeconds(1F);
        immunityFramesCooldownCheck = false;

    }

    public void Reset()
    {
        StartCoroutine(fade());
        IEnumerator fade()
        {
            GetComponent<Animator>().SetBool("IsDead", true);
            FindObjectOfType<FullMovementController_K>().canmove=false;
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForSeconds(0.02f);
                black.GetComponent<RawImage>().color += new Color(0, 0, 0, 0.05f);
            }
            transform.position = respawnObjectPosition;
            playerHealth = 50;
            electricityLevel = 100;

            //Destroy(currentEnemies);
            //currentEnemies = Instantiate(enemies, transform.parent = enemiesParent.transform);

            FindObjectOfType<GearScript>().canDie = true;
            GetComponent<Animator>().SetBool("IsDead", false);
            FindObjectOfType<FullMovementController_K>().canmove = true;
            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForSeconds(0.02f);
                black.GetComponent<RawImage>().color -= new Color(0, 0, 0, 0.05f);
            }
        }
    }
}
