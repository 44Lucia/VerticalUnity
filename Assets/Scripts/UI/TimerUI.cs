using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private int minutes, seconds, cents;

    public void setTimerTime(float time) 
    {
        if (time < 0){time = 0;}

        minutes = (int)(time / 60f);
        seconds = (int)(time - minutes * 60f);
        cents = (int)((time - (int)time) * 100f);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);
    }
}