using UnityEngine;
using System.Collections;
using UnityEngine.Pool;
using Unity.VisualScripting;

public class Bullet : MonoBehaviour
{
    public float speed;   // 총알 속도
    public float lifeTime; // 사라지기까지 시간
    public bool cutBullet; // 관통 여부

    public enum GravityType { Nomal, Fast, Slow, Stop }
    public GravityType gravityType = GravityType.Nomal;

    [Header("Nomal")]
    public float nomalFactor;

    [Header("Fast")]
    public float fastFactor;

    [Header("Slow")]
    public float slowFactor;

    [Header("Stop")]

    private ParticleSystem _particleSystem;
    private Rigidbody rb;

    public static Bullet instance { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _particleSystem = GetComponent<ParticleSystem>();
        
        if (instance != null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        Destroy(gameObject);
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

    //IEnumerator GrowEffect()
    //{

    //}

    public Rigidbody GravutyOption(GravityType type1, Rigidbody rb) 
    {
            switch (type1)
            { 
            case GravityType.Nomal:
                  rb.velocity *= nomalFactor;
                  return rb;
                
            case GravityType.Fast:
                  rb.velocity *= fastFactor;
                  return rb;

            case GravityType.Slow:
                  rb.velocity *= slowFactor;
                  return rb;

            case GravityType.Stop:
                  rb.velocity = Vector3.zero;
                  return rb;
            }

         Destroy(gameObject);
         return null;
    }
}
