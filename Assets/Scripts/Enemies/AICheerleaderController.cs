using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICheerleaderController : Enemy
{
    private StatesCheerleader currenState;

    [SerializeField] Transform partToRotate;
    [SerializeField] private float turnSpeed = 10f;

    [SerializeField] Transform target;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private LayerMask obstacleMask;

    [SerializeField] float EnemyRange = 20f;
    [SerializeField] private float distanceBetweenTarget;
    [SerializeField] private float countdownBetweenFire = 0f;
    [SerializeField] private float fireRate = 2f;

    protected override void Start()
    {
        base.Start();

        obstacleMask = LayerMask.GetMask("Obstacles");
        currenState = StatesCheerleader.IDLE;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch (currenState) 
        {
            case StatesCheerleader.IDLE:
                Idling();
                break;
            case StatesCheerleader.ATTACK:
                Shooting();
                break;
            case StatesCheerleader.DEATH:
                Dying();
                break;
        }      
    }

    private void Idling() 
    {
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= 8f)
        {
            currenState = StatesCheerleader.ATTACK;
            // DANCE ANIMATION
        }
    }

    private void LookAtTarget() 
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shooting() 
    {
        Vector3 dirToPlayer = (target.position - transform.position).normalized;
        float dstToPlayer = Vector3.Distance(transform.position, target.position);

        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 8f)
        {
            currenState = StatesCheerleader.IDLE;
        }

        //If there are no obstacles in between
        if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
        {
            distanceBetweenTarget = Vector3.Distance(target.position, transform.position);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                LookAtTarget();
                if (countdownBetweenFire <= 0f)
                {
                    foreach (Transform SpawnPoint in projectileSpawnPoint)
                    {
                        Instantiate(projectilePrefab, SpawnPoint.position, transform.rotation);
                    }

                    countdownBetweenFire = 1f / fireRate;
                }
                countdownBetweenFire -= Time.deltaTime;
            }
        }
    }

    private void Dying()
    {
        //Coorutina enlazada con el gamemanager
        Destroy(this.gameObject);
    }
}
