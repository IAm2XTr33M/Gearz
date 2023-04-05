using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScoreScreen : MonoBehaviour
{
    public string displayTime;
    // Start is called before the first frame update
    void Start()
    {
        displayTime = ScoreAndTimeController.endTIme.ToString("0.000");
        gameObject.GetComponent<TextMesh>().text = displayTime;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
