using UnityEngine;
using UnityEngine.AI;

public class AIOtakuController : MonoBehaviour
{
    private StatesOtaku currenState;

    private NavMeshAgent navMeshAgent;
    private float startWaitTime = 4;
    private float timeToRotate = 2;
    private float speedWalk = 3;
    private float speedRun = 6;

    private float viewRadius = 6;
    private float viewAngle = 90;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstacleMask;

    [SerializeField] private Transform[] waypoints;
    private int m_CurrentWaypointIndex;

    private Vector3 playerLastPosition = Vector3.zero;
    private Vector3 m_playerPositon;

    private float m_WaitTime;
    private float m_TimeToRotate;
    private bool m_PlayerInRange;
    private bool m_PlayerNear;
    private bool m_CaughtPlayer;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currenState = StatesOtaku.PATROL;
    }

    void Start()
    {
        m_playerPositon = Vector3.zero;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        playerMask = LayerMask.GetMask("Player");
        obstacleMask = LayerMask.GetMask("Obstacles");

        m_CurrentWaypointIndex = 0;

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    void Update()
    {
        EnviromentView();

        switch (currenState) 
        {
            case StatesOtaku.PATROL:
                Patroling();
                break;
            case StatesOtaku.CHASE:
                Chasing();
                break;
            case StatesOtaku.ATTACK:
                Attacking();
                break;
            case StatesOtaku.DEATH:
                Dying();
                break;
        }
    }

    private void Patroling() 
    {
        if (m_PlayerNear){
            if (m_TimeToRotate <= 0){
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else{
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance){
                if (m_WaitTime <= 0){
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else{
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void Chasing() 
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;

        if (!m_CaughtPlayer){
            Move(speedRun);
            navMeshAgent.SetDestination(m_playerPositon);
        }
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance){
            if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f) {
                currenState = StatesOtaku.PATROL;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else{
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f){
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

    private void CaughtPlayer() 
    {
        m_CaughtPlayer = true;
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
        if (Vector3.Distance(transform.position, player) <= 0.3){
            if (m_WaitTime <= 0){
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else{
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    private void EnviromentView() 
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++){
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle/2){
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask)){
                    m_PlayerInRange = true;
                    currenState = StatesOtaku.CHASE;
                }
                else{
                    m_PlayerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position)> viewRadius){
                m_PlayerInRange = false;
            }
            if (m_PlayerInRange){
                m_playerPositon = player.transform.position;
            }
        }
    }

    private void Attacking() 
    { 
        //Coorutina enlazada con el gamemanager
    }

    private void Dying() 
    {
        //Coorutina enlazada con el gamemanager
    }
}
