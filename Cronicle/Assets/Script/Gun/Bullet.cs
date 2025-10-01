using UnityEngine;
using System.Collections;
using UnityEngine.Pool;
using Unity.VisualScripting;

public class Bullet : MonoBehaviour
{
    public float speed;   // 총알 속도
    public float lifeTime; // 사라지기까지 시간
    public bool cutBullet; // 관통 여부

    public enum GravityType { Normal, Fast, Slow, Stop }
    public GravityType gravityType = GravityType.Normal;
    public test obstacle;

    [Header("Normal")]
    public float nomalFactor;

    [Header("Fast")]
    public float fastFactor;

    [Header("Slow")]
    public float slowFactor;

    [Header("Stop")]
    public float stopFactor;

    private ParticleSystem _particleSystem;
    private Rigidbody rb;

    public static Bullet instance { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _particleSystem = GetComponent<ParticleSystem>();
    }
    

    private void FixedUpdate()
    {
        // 발사 시 앞으로 힘을 준다
        rb.velocity = transform.forward * speed;
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

    private void OnTriggerEnter(Collider collider)
    {
        // 장애물과 중력이 충돌하면
        if (collider.gameObject.CompareTag("Obstacle")) 
        {
            test factor = collider.gameObject.GetComponent<test>();

            // 속성 값이 적용 된다 => 장애물 오브젝트에도 적용이 되는지?
            if (factor != null)
            {
                // 속성 값 적용
                switch (gravityType)
                {
                    case GravityType.Normal:
                        factor.ApplyGravityFactor(nomalFactor);
                        break;

                    case GravityType.Fast:
                        factor.ApplyGravityFactor(fastFactor);
                        break;

                    case GravityType.Slow:
                        factor.ApplyGravityFactor(slowFactor);
                        break;

                    case GravityType.Stop:
                        factor.ApplyGravityFactor(stopFactor);
                        break;
                }  
            }
            else
            {
                Debug.LogWarning("충돌한 obstacle에 Rigidbody가 없습니다!");
            }
        }  
    }
}
