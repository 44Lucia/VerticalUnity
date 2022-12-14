using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TimerUI timerUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject bosBar;

    public GameObject GetBosBarUI { get => bosBar; }
    public GameObject GetLoseUI { get => loseUI; }
    public TimerUI GetTimerUI { get => timerUI; }
    public GameObject GetWinUI { get => winUI; }
}
