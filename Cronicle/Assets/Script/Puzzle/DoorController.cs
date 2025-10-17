using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class DoorController : MonoBehaviour
{
    private HingeJoint hinge;
    private bool isOpen = false;

    public float openSpeed = 90f;
    public float motorForce = 500f;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useLimits = true;
        hinge.useMotor = true;

        JointMotor motor = hinge.motor;
        motor.force = motorForce;
        motor.freeSpin = false;
        motor.targetVelocity = 0f;
        hinge.motor = motor;
    }

    public void OpenDoor()
    {
        if (isOpen) return;
        isOpen = true;

        JointMotor motor = hinge.motor;
        motor.force = motorForce;
        motor.targetVelocity = Mathf.Abs(openSpeed); // Z축 양수 방향으로 열림
        hinge.motor = motor;
    }

    public void CloseDoor()
    {
        if (!isOpen) return;
        isOpen = false;

        JointMotor motor = hinge.motor;
        motor.force = motorForce;
        motor.targetVelocity = -Mathf.Abs(openSpeed); // Z축 음수 방향으로 닫힘
        hinge.motor = motor;
    }
}
