using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float speed = 100f;   // 총알 속도
    public float lifeTime = 10f; // 사라지기까지 시간
    public bool cutBullet; // 관통 여부

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // 발사 시 앞으로 힘을 준다
        _rigidbody.velocity = transform.forward * speed;
    }

    private void OnEnable()
    {
        // 3초 후 자동으로 비활성화 → 풀로 복귀
        Invoke(nameof(Disable), 3f);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke(); // 중복 호출 방지
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
