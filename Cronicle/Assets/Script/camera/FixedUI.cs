using UnityEngine;

public class FixedUI : MonoBehaviour
{
    public Transform target; // 따라다닐 오브젝트
    public Vector3 offset;   // 오브젝트에서의 상대 위치

    private Quaternion fixedRotation;

    void Start()
    {
        // 시작할 때 회전을 저장 (고정값)
        fixedRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 위치는 따라간다
        transform.position = target.position + offset;

        // 회전은 고정
        transform.rotation = fixedRotation;
    }
}
