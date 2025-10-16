using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class DoorController : MonoBehaviour
{
    private HingeJoint hinge;
    private JointSpring doorSpring;

    [Header("Door Open Settings")]
    public float openAngle = 90f;     // 문이 열릴 각도
    public float springForce = 50f;   // 문을 닫히게 하는 힘 (느슨하게 유지 가능)
    public float openSpeed = 10f;     // 문 열리는 속도
    public bool isOpen = false;

    private void Start()
    {
        hinge = GetComponent<HingeJoint>();
        doorSpring = hinge.spring;
        doorSpring.spring = springForce;
        doorSpring.damper = 5f;
        hinge.useSpring = true;
    }

    public void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;
        Debug.Log($"{name} 문 열림");

        // 목표 각도 설정
        doorSpring.targetPosition = -openAngle;  // 힌지 축 방향에 따라 + 또는 - 변경 필요
        hinge.spring = doorSpring;
    }

    public void CloseDoor()
    {
        if (!isOpen) return;

        isOpen = false;
        Debug.Log($"{name} 문 닫힘");

        doorSpring.targetPosition = 0f;
        hinge.spring = doorSpring;
    }
}
