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

    [SerializeField] float EnemyRange = 20f;
    [SerializeField] private float distanceBetweenTarget;
    [SerializeField] private float countdownBetweenFire = 0f;
    [SerializeField] private float fireRate = 2f;

    private Coroutine LookCoroutine;

    protected override void Start()
    {
        base.Start();

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
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 8f)
        {
            currenState = StatesCheerleader.IDLE;
        }

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

    private void Dying()
    {
        //Coorutina enlazada con el gamemanager
        Destroy(this.gameObject);
    }
}
