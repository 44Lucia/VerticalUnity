using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITutorial : Enemy
{
    [SerializeField] private float m_maxHealth = 3;
    [SerializeField] private float m_health = 3;

    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject activateLight;

    [Header("Music")]
    [SerializeField] private AudioClip m_onHitClip;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        health = m_health;
        maxHealth = m_maxHealth;
        impactEffect = deathEffect;
        lightOn = activateLight;

        onHitClip = m_onHitClip;

        activateLight.SetActive(false);
    }
}
