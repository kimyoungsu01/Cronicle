using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    [Header("Climb Settings")]
    public float climbSpeed = 3f;     // 오르내리는 속도
    public bool isClimbing = false;   // 사다리 타는 중인지
    public bool isNearLadder = false; // 사다리 근처인지

    private Rigidbody rb;
    private Vector3 ladderForward;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    void Update()
    {
        // E 키로 사다리 타기 시작
        if (isNearLadder && Input.GetKeyDown(KeyCode.E))
        {
            StartClimb();
        }

        // 스페이스바로 사다리 타기 종료
        if (isClimbing && Input.GetKeyDown(KeyCode.Space))
        {
            StopClimb();
        }

        if (isClimbing)
        {
            ClimbMovement();
        }
    }

    private void StartClimb()
    {
        isClimbing = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        Debug.Log("사다리 타기 시작");
    }

    private void StopClimb()
    {
        isClimbing = false;
        rb.useGravity = true;
        Debug.Log("사다리 타기 종료");
    }

    private void ClimbMovement()
    {
        float vertical = Input.GetAxis("Vertical"); // W/S 키 입력
        Vector3 move = Vector3.up * vertical * climbSpeed;

        rb.velocity = move;

        // 사다리 바라보는 방향으로 회전 (정렬)
        if (ladderForward != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(-ladderForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isNearLadder = true;
            ladderForward = other.transform.forward;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isNearLadder = false;
            StopClimb();
        }
    }
}
