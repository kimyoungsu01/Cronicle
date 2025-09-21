using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public MoveObsculer obstacleData; // ScriptableObject 참조
    private int direction;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody = gameObject.AddComponent<Rigidbody>();
    }

    private void Start()
    {
        if (obstacleData == null)
        {
            Debug.LogError("Obstacle Data가 없습니다!");
            return;
        }
    }

    private void FixedUpdate()
    {
        if (obstacleData == null) return;

        // 좌우 이동
        _rigidbody.MovePosition(transform.position + Vector3.right * direction * obstacleData.moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 벽, 다른 장애물과 부딪히면 방향 반전
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Obstacle"))
        {
            direction *= -1;
        }

        // 플레이어가 발판에 올라섰을 때
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform); // 발판의 자식으로
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 발판에서 벗어나면 부모 해제
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}


