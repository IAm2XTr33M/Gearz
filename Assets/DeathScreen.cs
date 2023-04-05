using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public RawImage bg;
    public TextMeshProUGUI DeathText;
    public TextMeshProUGUI DescText;
    public TextMeshProUGUI SpaceText;

    bool isDead = false;


    private void Update()
    {
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                restart();
            }
        }
    }

    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Die()
    {
        bg.gameObject.SetActive(true);
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.05f);
            ChangeOpacity(0.05f);
        }
        this.isDead = true;
    }

    void ChangeOpacity(float o)
    {
        bg.color = new Color(1, 0, 0, bg.color.a+ o + o);
        DeathText.color = new Color(1, 0, 0, DeathText.color.a + o);
        DescText.color = new Color(1, 0, 0, DescText.color.a + o);
        SpaceText.color += new Color(1, 1, 1, o);
    }
}
