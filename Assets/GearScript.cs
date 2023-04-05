using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GearScript : MonoBehaviour
{
    [Header("References")]
    public GameObject mainGear;
    public GameObject smallGear1;
    public GameObject smallGear2;
    public GameObject smallGear3;
    public Player_Health_System_T health;

    [Header("Speed")]
    public float rotationSpeed;
    int[] brokenSpeed = new int[] { 1, 1, 1};


    [System.Serializable]
    public class SpriteClass
    {
        public Sprite Gear0;
        public Sprite Gear1;
        public Sprite Gear2;
        public Sprite Gear3;
        public Sprite Gear4;
        public Sprite Gear5;
        public Sprite GearSmall;
        public Sprite GearSmallBroken;
    }
    [Header("Sprites")]
    public SpriteClass Sprites;

    public bool canDie = true;

    private void Start()
    {
        health = FindObjectOfType<Player_Health_System_T>();

        //Determine rotation side,
        int rnd = Random.Range(0, 2);
        if(rnd == 0)
        {
            rotationSpeed = -rotationSpeed;
        }
    }

    private void Update()
    {
        //Change Main Gear Sprite Depending on health
        ChangeSprite();

        //Rotate Main Gear.
        mainGear.transform.localEulerAngles -= new Vector3(0, 0, rotationSpeed * Time.deltaTime);

        //Rotate Small Gears
        smallGear1.transform.localEulerAngles -= new Vector3(0, 0, rotationSpeed * Time.deltaTime * brokenSpeed[0]);
        smallGear2.transform.localEulerAngles += new Vector3(0, 0, rotationSpeed * Time.deltaTime * brokenSpeed[1]);
        smallGear3.transform.localEulerAngles -= new Vector3(0, 0, rotationSpeed * Time.deltaTime * brokenSpeed[2]);
    }

    void ChangeSprite()
    {
        //Dont mind the spaghetti code

        if (health.playerHealth > 40)
        {
            mainGear.GetComponent<Image>().sprite = Sprites.Gear0;
        }
        else if (health.playerHealth < 41 && health.playerHealth > 30)
        {
            mainGear.GetComponent<Image>().sprite = Sprites.Gear1;
        }
        else if (health.playerHealth < 31 && health.playerHealth > 20)
        {
            mainGear.GetComponent<Image>().sprite = Sprites.Gear2;
        }
        else if (health.playerHealth < 21 && health.playerHealth > 10)
        {
            mainGear.GetComponent<Image>().sprite = Sprites.Gear3;
        }
        else if (health.playerHealth < 11 && health.playerHealth > 0)
        {
            mainGear.GetComponent<Image>().sprite = Sprites.Gear4;
        }
        else if (health.playerHealth < 1 && canDie)
        {
            mainGear.GetComponent<Image>().sprite = Sprites.Gear5;
            LoseOneGear();
        }
    }

    public void LoseOneGear()
    {
        canDie = false;
        if (brokenSpeed[1] == 1)
        {
            if (brokenSpeed[2] == 1)
            {
                smallGear3.GetComponent<Image>().sprite = Sprites.GearSmallBroken;
                brokenSpeed[2] = 0;
            }
            else
            {
                smallGear2.GetComponent<Image>().sprite = Sprites.GearSmallBroken;
                brokenSpeed[1] = 0;
            }
        }
        else
        {
            smallGear1.GetComponent<Image>().sprite = Sprites.GearSmallBroken;
            brokenSpeed[0] = 0;
        }
        health.playerLives--;
        if(health.playerLives <= 0)
        {
            FindObjectOfType<DeathScreen>().Die();
        }
        else
        {
            health.Reset();
        }
    }
}
