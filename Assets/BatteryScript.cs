using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    Player_Health_System_T system;

    private void Start()
    {
        system = FindObjectOfType<Player_Health_System_T>();
    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(0,8+ (82 * system.electricityLevel / 100), 0);

        if(system.electricityLevel <= 0)
        {
            GearScript gearScript = FindObjectOfType<GearScript>();
            gearScript.LoseOneGear();
        }
    }
}
