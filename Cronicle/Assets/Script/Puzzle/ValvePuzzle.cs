using UnityEngine;

public class ValvePuzzle : MonoBehaviour
{
    [Header("퍼즐 설정")]
    public bool isCompleted = false;
    public DoorController linkedDoor;
    public Transform player;
    public float interactDistance = 2f;

    [Header("회전 관련")]
    public float rotationSpeed = 45f;
    public float requiredRotation = 45f;
    private float currentRotation = 0f;
    private bool isRotating = false;

    void Update()
    {
        if (isCompleted) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= interactDistance)
                isRotating = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
            isRotating = false;

        if (isRotating)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, step);
            currentRotation += step;

            if (currentRotation >= requiredRotation)
                CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        isCompleted = true;
        isRotating = false;
        Debug.Log($"{name} 퍼즐 완료!");

        if (linkedDoor != null)
            linkedDoor.OpenDoor();
    }
}
