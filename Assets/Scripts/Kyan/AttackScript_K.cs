using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript_K : MonoBehaviour
{
    GameObject damageOBJ;

    public float hitDamage;
    public float hitDelay;
    public float hitDuration;
    public float hitRange;
    bool canHit = true;

    public KeyCode AttackKey;
    private void Start()
    {
        damageOBJ = GameObject.Find("DamageOBJ");
    }

    private void Update()
    {
        if(Input.GetKeyDown(AttackKey) && canHit)
        {
            canHit = false;
            StartCoroutine(hit());
        }
    }

    IEnumerator hit()
    {
        GetComponent<Animator>().SetTrigger("Attack");

        damageOBJ.transform.localScale += new Vector3(hitRange, 0, 0);
        yield return new WaitForSeconds(hitDuration);
        damageOBJ.transform.localScale -= new Vector3(hitRange, 0, 0);            

        yield return new WaitForSeconds(hitDelay);
        canHit = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !canHit)
        {
            Enemy_Base_Script_T enemyBase = collision.GetComponent<Enemy_Base_Script_T>();
            if (enemyBase)
            {
                enemyBase.amountOfHealth -= hitDamage;
                StartCoroutine(dmg());
                IEnumerator dmg()
                {
                    Color old = collision.GetComponent<SpriteRenderer>().color;
                    collision.GetComponent<SpriteRenderer>().color = Color.red;
                    yield return new WaitForSeconds(0.2f);
                    collision.GetComponent<SpriteRenderer>().color = old;
                }
            }
        }
    }
}
