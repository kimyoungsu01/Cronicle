using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Idle,
    Wandering, // 임의 목표 지점
    Attacking,
    Eat 
}

public class NPC : MonoBehaviour, IDamageable
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
    public int damage = 9999; // 즉사
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;

    private float playerDistance;
    private int health = 100;
    private bool isEating = false;

    public float fieldOfView = 120f; // 시야각

    private Animator animator;
    private PlayerController playerController;
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
        if (CharacterManager.instance == null || CharacterManager.instance.Player == null) return;

        playerDistance = Vector3.Distance(transform.position, CharacterManager.instance.Player.transform.position);
        animator.SetBool("Moving", aiState != AIState.Idle);

        // 플레이어 감지 (시야 + 거리)##
        if (playerDistance < detectDistance && IsPlayerInFieldOfView())
        {
            SetState(AIState.Attacking); // 감지되면 달려오기 시작##
        }

        switch (aiState)
        {
            case AIState.Idle:
                break;

            case AIState.Wandering:
                PassiveUpdate();
                break;

            case AIState.Attacking:
                AttackingUpdate();
                break;

            case AIState.Eat:
                break;
        }
    }

    public void SetState(AIState state)
    {
        aiState = state;

        switch (aiState)
        {
            case AIState.Idle:
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
            
            case AIState.Eat:
                navMeshAgent.isStopped = true;
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
        navMeshAgent.SetDestination(GetWanderLocation());
    }

    Vector3 GetWanderLocation()
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
                // 애니메이션 트리거 (물어뜯기 준비)
                animator.SetTrigger("Bite");
                SetState(AIState.Eat);
                isEating = true;

                // 플레이어 정지
                var playerController = CharacterManager.instance.Player.controller;
                if (playerController != null)
                    playerController.enabled = false;

                // NPC도 완전 멈춤
                navMeshAgent.isStopped = true;
                navMeshAgent.velocity = Vector3.zero;
            }
        }
        else if (playerDistance < detectDistance)
        {
            navMeshAgent.SetDestination(CharacterManager.instance.Player.transform.position);
        }
        else
        {
            SetState(AIState.Wandering);
        }
    }

    bool IsPlayerInFieldOfView()
    {
        if (CharacterManager.instance == null || CharacterManager.instance.Player == null)
            return false;

        Vector3 directionToPlayer = (CharacterManager.instance.Player.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        // 시야각 안에 들어오면 true 반환##
        return angle < fieldOfView * 0.5f;
    }


    public void OnBiteHit() // 애니메이션 이벤트에서 호출
    {
        if (CharacterManager.instance == null || CharacterManager.instance.Player == null) return;

        var player = CharacterManager.instance.Player.controller.GetComponent<IDamageable>();
        player?.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            animator.SetTrigger("Die");
            navMeshAgent.isStopped = true;
            this.enabled = false;
        }
    }

}
