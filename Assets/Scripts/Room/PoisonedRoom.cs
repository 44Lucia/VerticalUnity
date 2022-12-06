using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoisonedRoom : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField, Tooltip("Time in seconds")] private float timerTime;

    private int minutes, seconds, cents;

    private bool m_coolDownDamage;

    private void Awake()
    {
        m_coolDownDamage = false;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timerTime -= Time.deltaTime;

        if (timerTime < 0) { timerTime = 0; }

        minutes = (int)(timerTime / 60f);
        seconds = (int)(timerTime - minutes * 60f);
        cents = (int)((timerTime - (int)timerTime) * 100f);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);

        if (timerTime == 0)
        {
            if (!m_coolDownDamage)
            {
                //Lanzar evento
                GameManager._GAME_MANAGER.DamagePlayer(1);
                StartCoroutine(HitPlayer());
            }
            
        }
    }

    public IEnumerator HitPlayer() 
    {
        m_coolDownDamage = true;
        yield return new WaitForSeconds(10f);
        m_coolDownDamage = false;
    }
}
