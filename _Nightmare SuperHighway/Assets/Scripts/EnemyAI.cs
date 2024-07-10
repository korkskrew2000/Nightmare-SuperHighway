using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    #region Public States
    public EnemyState enemyState;
    public bool constantRoam;
    public float alertedRange = 20f;
    public float cautionRange = 40f;
    public bool caution;
    public Transform[] roamingPoints;
    public int currentRoamPoint;
    public float roamingSpeed = 5f;
    public float chaseSpeed = 10f;
    #endregion

    #region Private States
    bool playerInSight;
    Transform player;
    NavMeshAgent agent;
    LayerMask ground = 1 << 6;
    LayerMask playerMask = 1 << 3;
    #endregion

    public enum EnemyState {
        Idle,
        Roam,
        Chase
    }

    void Start() {
        player = GameManager.Instance.player.gameObject.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        switch (enemyState) {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Roam:
                agent.speed = roamingSpeed;
                Patrolling();
                break;
            case EnemyState.Chase:
                agent.speed = chaseSpeed;
                ChasePlayer();
                break;
        }
        caution = Physics.CheckSphere(transform.position, cautionRange, playerMask);
        playerInSight = Physics.CheckSphere(transform.position, alertedRange, playerMask);
    }

    void Idle() {
        //Stay completely still until player is within caution range
        if (constantRoam) {
            enemyState = EnemyState.Roam;
        } else {
            transform.position = this.transform.position;
            if (caution) {
                enemyState = EnemyState.Roam;
            }
        }
    }

    void Patrolling() {
        //Enemy goes between set walking points in an order until finds player
        agent.SetDestination(roamingPoints[currentRoamPoint].position);
        for (int i = 0; i < roamingPoints.Length; i++) {
            if (Vector3.Distance(transform.position, roamingPoints[currentRoamPoint].position) < 1f) {
                currentRoamPoint++;
                if (currentRoamPoint > roamingPoints.Length-1){
                    currentRoamPoint = 0;
                }
            }
        }
        if (playerInSight) {
            enemyState = EnemyState.Chase;
        }
    }

    void ChasePlayer() {
        agent.SetDestination(player.position);
        if (playerInSight == false) {
            enemyState = EnemyState.Roam;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alertedRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, cautionRange);
    }
}
