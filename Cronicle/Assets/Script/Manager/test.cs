using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class test : MonoBehaviour
{
    [Header("발판 이동 설정")]
    public float speed = 2f;                  // 이동 속도
    public Vector3 baseDirection = Vector3.forward; // 이동 기본 방향
    public float minDistance = 3f;            // 최소 왕복 거리
    public float maxDistance = 8f;            // 최대 왕복 거리

    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 currentDir;
    private Vector3 moveDelta;
    private float distance;                    // 이번 발판의 왕복 거리

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        startPos = transform.position;

        // 랜덤 왕복 거리 설정
        distance = Random.Range(minDistance, maxDistance);

        // 랜덤 이동 방향 결정
        float randomSign = Random.value < 0.5f ? 1f : -1f;
        currentDir = baseDirection.normalized * randomSign;
    }

    void FixedUpdate()
    {
        // 발판 이동
        Vector3 newPos = transform.position + currentDir * speed * Time.fixedDeltaTime;
        moveDelta = newPos - transform.position;
        rb.MovePosition(newPos);

        // 왕복 거리 체크
        float traveled = Vector3.Distance(startPos, newPos);
        if (traveled >= distance)
        {
            currentDir *= -1;          // 이동 방향 반전
            startPos = transform.position;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody prb = collision.collider.GetComponent<Rigidbody>();
            if (prb != null)
            {
                // 플레이어 이동량에 발판 이동량 더함
                prb.MovePosition(prb.position + moveDelta);
            }
        }
    }

    public void ApplyGravityFactor(float factor)
    {
        speed = factor;
        Debug.Log($"[Obstacle] Speed 변경됨 → {speed}");
    }
}




