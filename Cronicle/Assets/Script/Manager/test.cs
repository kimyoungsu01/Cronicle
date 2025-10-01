using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class test : MonoBehaviour
{
    public float speed;
    public float distance;

    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 moveDir = Vector3.forward;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * speed ;

        // 왕복 거리 체크
        float offset = Vector3.Distance(transform.position, startPos); // -4,
        if (Mathf.Abs(offset) >= distance)
        {
            moveDir *= -1; // 방향 반전
            startPos = transform.position; // 기준 위치 갱신
        }
    }

    public void ApplyGravityFactor(float factor)
    {
        speed = factor;
        Debug.Log($"[Obstacle] Speed 변경됨 → {speed}");
    }
}
