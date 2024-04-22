using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // TODO fix speed, define speed variables faster when aggroed, slower when patrolling, fast af when resetting
    public float leashRadius = 30.0f;

    [Header("Patrolling")]
    public bool randomPatrol = false;
    public float patrolRange;
    public Transform patrolOrigin;
    public float patrolPauseTime = 2.0f; // Pause before moving to next patrol point.
    
    [Header("Targetting")]
    public float aggroRadius = 10.0f;
    [Range(1,360)]
    public float viewAngle = 45;
    public bool canSeePlayer;
    public float attackRange = 4;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;
    CharacterStats stats;

    bool aggroed = false;
    bool resetting = false;

    Vector3 aggroPoint;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        aggroPoint = transform.position;
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<EnemyStats>();
    }

    bool Patrol() {
        // TODO add a way to pause the random patrol in between new points.
        if (agent.remainingDistance <= agent.stoppingDistance) {
            Vector3 point;
            if (RandomPoint(patrolOrigin.position, patrolRange, out point)) {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);               
                agent.SetDestination(point);
                agent.speed = 1;
                return true;
            }
        }
        return false;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result) {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public void Aggro(Transform player) {
        if (!aggroed && !resetting) {
            target = player;
            aggroed = true;
            aggroPoint = transform.position;
            agent.speed = 6;
        }
    }

    void Update()
    {
        // If patrolling
        if (!aggroed && !resetting && randomPatrol) {
            Patrol();
        }

        float distance = Vector3.Distance(target.position, transform.position);
        float distanceToSpawn = Vector3.Distance(aggroPoint, transform.position);
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        // Enemy reset, can be aggroed again
        if (distanceToSpawn <= 2 && resetting) {
            resetting = false;
            agent.speed /= 2;
            stats.SetImmune(false);
        }

        // Player aggroed this enemy, move to the player
        if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2) {
            if (distance <= aggroRadius && !resetting && !aggroed) {
                aggroed = true;
                agent.speed = 6;
                aggroPoint = transform.position;
            }
        }

        // TODO Hearing based aggro?
        // https://www.youtube.com/watch?v=ho7-pVNU62g

        if (aggroed) {
            agent.SetDestination(target.position);
            
            if (distance <= attackRange) {
                // attack
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null) {
                    combat.Attack(targetStats);
                }
                // face target
                FaceTarget(target.position);
            }

            // If the player dragged the enemy to far away, leash it back
            if (distanceToSpawn >= leashRadius){
                agent.SetDestination(aggroPoint);
                agent.speed *= 2;
                FaceTarget(aggroPoint);
                aggroed = false;
                resetting = true;
                stats.ResetHealth();
                stats.SetImmune(true);
            }
        }

    }

    void FaceTarget(Vector3 target) {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.x));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1f);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(aggroPoint, leashRadius);

        Vector3 viewAngle1 = DirectionFromAngle(transform.eulerAngles.y, -viewAngle / 2);
        Vector3 viewAngle2 = DirectionFromAngle(transform.eulerAngles.y, viewAngle / 2);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + viewAngle1 * aggroRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngle2 * aggroRadius);
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees) {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
