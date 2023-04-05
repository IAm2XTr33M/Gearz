using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathScript : MonoBehaviour
{
    public bool CanDie;

    Enemy_Base_Script_T baseScript;


    void Start()
    {
        baseScript = GetComponent<Enemy_Base_Script_T>();
    }

    void Update()
    {
        if(baseScript.amountOfHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
