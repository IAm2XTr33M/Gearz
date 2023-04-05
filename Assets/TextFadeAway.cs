using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFadeAway : MonoBehaviour
{
    bool hasRead = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !hasRead)
        {
            hasRead = true;
            StartCoroutine(startFade());
        }

        IEnumerator startFade()
        {
            TextMeshPro Text = GetComponent<TextMeshPro>();
            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForSeconds(0.025f);
                Text.color -= new Color(0, 0, 0, 0.05f);
            }
        }
    }
}
