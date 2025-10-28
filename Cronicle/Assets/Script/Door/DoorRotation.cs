using UnityEngine;

public class DoorRotation : MonoBehaviour
{
    [Header("문 회전 설정")]
    public float openAngle = 90f;         // 열릴 각도
    public float openSpeed = 5f;          // 부드럽게 열리고 닫히는 속도

    [Header("플레이어 감지")]
    public float interactDistance = 3f;   // E키 반응 거리
    public Transform player;              // 플레이어 Transform 연결 필수

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion targetRotation;

    void Start()
    {
        if (player == null)
            Debug.LogError(" DoorController_Full: Player Transform이 연결되지 않았습니다!");

        closedRotation = transform.rotation;
        targetRotation = closedRotation;
    }

    void Update()
    {
        if (player == null) return;

        // 플레이어와 거리 체크
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= interactDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleDoor();
                // 문 열림 효과음 재생
                SoundManager.Instance.PlaySFX(SoundManager.Instance.lagedoor);
            }
        }

        // 부드럽게 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
    }

    void ToggleDoor()
    {
        if (!isOpen)
        {
            targetRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
            isOpen = true;
        }
        else
        {
            targetRotation = closedRotation;
            isOpen = false;
        }

        Debug.Log("Door toggled! Open: " + isOpen);
    }

    // 상호작용 범위 시각화
    void OnDrawGizmosSelected()
    {
        if (player == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
}
