using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreAndTimeController : MonoBehaviour
{
    public static float timePlayed;
    public static float endTIme;
    public GameObject pauseIcon;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseIcon.GetComponent<RawImage>().enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseIcon.GetComponent<RawImage>().enabled = false;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        timePlayed = Time.time;
        //Debug.Log(timePlayed);
        //Debug.Log(endTIme);
    }
    public static void TotalTime()
    {
        endTIme = timePlayed;
    }
}
