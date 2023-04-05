using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FireAnimator : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>();

    private void Start()
    {
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        yield return new WaitForSeconds(0.1f * Random.Range(0.5f,1.5f));
        int rnd = Random.Range(0, Sprites.Count);
        GetComponent<SpriteRenderer>().sprite = Sprites[rnd];
        StartCoroutine(Animate());
    }
}
