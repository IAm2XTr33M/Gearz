using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupMain_K : MonoBehaviour
{
    public TextMeshProUGUI PopUpText;

    public bool PopUpShowing;
    public float FadeProcent;

    public float Timer;
    public float duration;
    public bool ShowText = false;

    private void Update()
    {
        if(ShowText == true)
        {
            Timer += Time.deltaTime;

            float CurrentProcent = Timer / duration * 100;

            if (CurrentProcent < FadeProcent)
            {
                float opacity = CurrentProcent / FadeProcent;
                PopUpText.color = new Color(PopUpText.color.r, PopUpText.color.g, PopUpText.color.b, opacity);
            }
            else if (CurrentProcent > 100-FadeProcent)
            {
                float opacity = CurrentProcent - 100 - FadeProcent / FadeProcent;
                PopUpText.color = new Color(PopUpText.color.r, PopUpText.color.g, PopUpText.color.b, opacity);
            }
            if(Timer > duration)
            {
                ShowText = false;
                Timer = 0;
            }
        }    
    }

    private void Start()
    {
        CreatePopUp("Welcome to gamename",20);
    }

    public void CreatePopUp(string text,float duration)
    {
        PopUpText.text = text;
        this.duration = duration;
        ShowText = true;
    }
}
