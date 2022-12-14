using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISpiderController : Enemy
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float startWaitTime = 1;
    [SerializeField] private float timeToRotate = 2;
    [SerializeField] private float speedWalk = 3;
    [SerializeField] private GameObject deathEffect;

    [SerializeField] private Transform[] waypoints;
    private int m_CurrentWaypointIndex;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float countdownBetweenFire = 0f;
    [SerializeField] private float fireRate = 0.5f;

    private Vector3 playerLastPosition = Vector3.zero;

    private float m_WaitTime;
    private float m_TimeToRotate;
    private bool m_PlayerNear;

    [Header("Music")]
    [SerializeField] private AudioClip m_onHitClip;

    protected override void Start()
    {
        base.Start();

        navMeshAgent = GetComponent<NavMeshAgent>();

        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;

        onHitClip = m_onHitClip;
        impactEffect = deathEffect;

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {


        Patroling();
    }

    private void Patroling()
    {
        if (m_PlayerNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            if (countdownBetweenFire <= 0f)
            {
                Instantiate(bulletPrefab, projectileSpawnPoint.position, transform.rotation);
                countdownBetweenFire = 1f / fireRate;
            }
            countdownBetweenFire -= Time.deltaTime;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
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

    private void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    private void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }
}
