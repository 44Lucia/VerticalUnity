using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICheerleaderController : Enemy
{
    private StatesCheerleader currenState;

    [SerializeField] Transform target;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] float EnemyRange = 20f;
    private float distanceBetweenTarget;
    private float countdownBetweenFire = 0f;
    private float fireRate = 2f;

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

        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= 8f)
        {
            currenState = StatesCheerleader.ATTACK;
        }
    }

    private void Idling() 
    { 
        //Animacion bailando
    }

    private void StartRotating() 
    {
        if (LookCoroutine != null){
            StopCoroutine(LookCoroutine);
        }
        LookCoroutine = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt() 
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);

        float time = 0;

        while (time < 1) 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * 1;

            yield return null;
        }
    }

    private void Shooting() 
    {
        distanceBetweenTarget = Vector3.Distance(target.position, transform.position);
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (countdownBetweenFire <= 0f)
            {
                StartRotating();

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
