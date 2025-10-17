using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Idle,
    Wandering, // 임의 목표 지점
    Attacking
}

public class NPC : MonoBehaviour,IDamageable
{
    [Header("Status")]
    public float walkSpeed;
    public float runSpeed;

    [Header("AI")]
    private NavMeshAgent navMeshAgent;
    public float detectDistance; // 플레이어 감지 거리(최소 || 최대거리 계산)
    private AIState aiState;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    [Header("Combat")]
    public int damage;
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;

    private float playerDistance;

    public float fieldOfView = 120f; // 시야각

    private Animator animator;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(AIState.Wandering);
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(transform.position, CharacterManager.instance.player.transform.position);

        animator.SetBool("Moving", aiState != AIState.Idle);

        switch (aiState)
        {
            case AIState.Idle:
                break;

            case AIState.Wandering:
                break;

            case AIState.Attacking:
                break;
        }
    }

    public void SetState(AIState state)
    {
        aiState = state;

        switch (aiState)
        {
            case AIState.Idle:
                navMeshAgent.speed = walkSpeed;
                navMeshAgent.isStopped = true;
                break;

            case AIState.Wandering:
                navMeshAgent.speed = walkSpeed;
                navMeshAgent.isStopped = false;
                break;

            case AIState.Attacking:
                navMeshAgent.speed = runSpeed;
                navMeshAgent.isStopped = false;
                break;
        }

        animator.speed = navMeshAgent.speed / walkSpeed;
    }

    void PassiveUpdate()
    {
        if (aiState == AIState.Wandering && navMeshAgent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }
    }

    void WanderToNewLocation()
    {
        if (aiState != AIState.Idle) return;

        SetState(AIState.Wandering);
        navMeshAgent.SetDestination(GetRandomWanderLocation());
    }

    Vector3 GetRandomWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;

        while (Vector3.Distance(transform.position, hit.position) < minWanderDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;

            if (i > 30) break;
        }
        return hit.position;
    }

    void AttackingUpdate()
    {
        if (playerDistance < attackDistance && IsPlayerInFieldOfView())
        {
            navMeshAgent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate) 
            { 
               lastAttackTime = Time.time;
                CharacterManager.instance.player.controller.GetComponent<IDamageable>().TakeDamage(damage);
                animator.speed = 1f;
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            if(playerDistance < detectDistance)
            {
                navMeshAgent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (navMeshAgent.CalculatePath(CharacterManager.instance.player.transform.position, path)) 
                { 
                   navMeshAgent.SetDestination(CharacterManager.instance.player.transform.position);
                }
            }

            else
            {
                navMeshAgent.SetDestination(transform.position);
                navMeshAgent.isStopped = true;
                SetState(AIState.Wandering);
            }
        }
    }

    bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = CharacterManager.instance.player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return false;
    }

    public void TakeDamage(int amount)
    {
        
    }
}
