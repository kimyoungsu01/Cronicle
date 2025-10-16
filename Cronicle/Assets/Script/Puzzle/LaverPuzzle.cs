using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [Header("퍼즐 설정")]
    public bool isCompleted = false;
    public DoorController linkedDoor;
    public Transform player;
    public float interactDistance = 2f;

    [Header("레버 회전 관련")]
    public float rotationSpeed = 60f;
    public float requiredRotation = 90f;
    private float currentRotation = 0f;
    private bool isPulling = false;

    void Update()
    {
        if (isCompleted) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= interactDistance)
                isPulling = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
            isPulling = false;

        if (isPulling)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(step, 0, 0); // X축 회전 (레버 내림)
            currentRotation += step;

            if (currentRotation >= requiredRotation)
                CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        isCompleted = true;
        isPulling = false;
        Debug.Log($"{name} 레버 퍼즐 완료!");

        if (linkedDoor != null)
            linkedDoor.OpenDoor();
    }
}
