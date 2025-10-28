using UnityEngine;

public class FixedDirectionCamera : MonoBehaviour
{
    public Transform target;          // 따라갈 대상 (예: 플레이어)
    public Vector3 offset = new Vector3(0, 5, -8);
    public Vector3 fixedForward = Vector3.forward; // 항상 바라볼 방향 (Z+)

    void LateUpdate()
    {
        if (!target) return;

        // 대상 기준으로 위치 이동
        transform.position = target.position + offset;

        // 항상 한 방향으로 고정 회전
        transform.rotation = Quaternion.LookRotation(fixedForward, Vector3.up);
    }
}
