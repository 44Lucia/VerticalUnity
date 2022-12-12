using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AIFinalBos : Enemy
{
    private StatesFinalBos currenState;

    private NavMeshAgent navMeshAgent;

    //Timer para que pase de persecución a a descanso
    private float speedWalk = 3;
    [SerializeField] private float tiredTime;
    private bool coolDownChase;
    private Vector3 m_playerPositon;

    //Spawn meteor
    [Header("Meteors")]
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 size;
    [SerializeField] GameObject meteorPrefab;
    private bool isAttack2;

    //Health
    [Header("Health")]
    [SerializeField] private float m_maxHealth = 20;
    [SerializeField] private float m_health = 20;
    [SerializeField] private GameObject healthBosContainer;
    [SerializeField] private float fillValue;
    [SerializeField] private GameObject lifeBar;
    [SerializeField] private GameObject deathEffect;

    [SerializeField] private LayerMask obstacleMask;
    private float viewRadius = 300;
    private float viewAngle = 90;
    private bool m_PlayerInRange;
    [SerializeField] private LayerMask playerMask;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currenState = StatesFinalBos.ATTACK;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        lifeBar.SetActive(true);
        playerMask = LayerMask.GetMask("Player");
        obstacleMask = LayerMask.GetMask("Obstacles");
        m_playerPositon = Vector3.zero;
        m_PlayerInRange = false;
        coolDownChase = false;
        isAttack2 = false;
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;

        impactEffect = deathEffect;
        health = m_health;
        maxHealth = m_maxHealth;

        coolDown = 2;
    }

    // Update is called once per frame
    void Update()
    {
        EnviromentView();
        UpdateLifeBar();

        switch (currenState) 
        {
            case StatesFinalBos.TIRED:
                Tired();
                if (isAttack2){ MeteorAttack(); }
                break;
            case StatesFinalBos.ATTACK:
                MeleeAttack();
                if (isAttack2){ MeteorAttack(); }
                break;
            case StatesFinalBos.DEATH:
                break;
        }

        Debug.Log(currenState);
    }

    private void UpdateLifeBar()
    {
        fillValue = health;
        fillValue = fillValue / maxHealth;
        healthBosContainer.GetComponent<Image>().fillAmount = fillValue;
    }

    private void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask) && !coolDownChase)
                {
                    m_PlayerInRange = true;
                    currenState = StatesFinalBos.ATTACK;
                }
                else
                {
                    m_PlayerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_PlayerInRange = false;
            }
            if (m_PlayerInRange)
            {
                m_playerPositon = player.transform.position;
            }
        }

        if (health <= 10)
        {
            isAttack2 = true;
        }
    }

    private void Tired() 
    {
        Stop();
        tiredTime -= Time.deltaTime;
        if (tiredTime <= 0)
        {
            Debug.Log("cansado");
            currenState = StatesFinalBos.ATTACK;
            coolDownChase = false;
        }
       
    }

    private void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    private void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    private void MeleeAttack()
    {
        if (!coolDownChase)
        {
            Debug.Log("buscando");
            Move(speedWalk);
            navMeshAgent.SetDestination(m_playerPositon);
            tiredTime += Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= 1f && !coolDownAttack)
        {
            GameManager._GAME_MANAGER.DamagePlayer(1);
            StartCoroutine(CoolDown());
        }

        if (tiredTime >= 5f)
        {
            coolDownChase = true;
            currenState = StatesFinalBos.TIRED;
        }
    }

    private void MeteorAttack()
    {
        if (!coolDownAttack)
        {
            SpawnMeteor();
            StartCoroutine(CoolDown());
        }
    }

    private void SpawnMeteor() 
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));

        Instantiate(meteorPrefab, pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(center, size);
    }
}
