using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITutorial : Enemy
{
    [SerializeField] private float m_maxHealth = 8;
    [SerializeField] private float m_health = 8;

    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject activateLight;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        health = m_health;
        maxHealth = m_maxHealth;
        impactEffect = deathEffect;

        activateLight.SetActive(false);
    }
}
