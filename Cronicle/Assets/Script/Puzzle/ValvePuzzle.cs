using UnityEngine;

public class ValvePuzzle : MonoBehaviour
{
    public bool isCompleted = false;
    public DoorController linkedDoor;
    public Transform player;
    public float interactDistance = 2f;
    public float rotationSpeed = 45f;
    public float requiredRotation = 45f;
    private float currentRotation = 0f;
    private bool isRotating = false;

    void Update()
    {
        if (isCompleted) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector3.Distance(player.position, transform.position) <= interactDistance)
                isRotating = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
            isRotating = false;

        if (isRotating)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, step); // ZÃà È¸Àü
            currentRotation += step;

            if (currentRotation >= requiredRotation)
                CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        isCompleted = true;
        isRotating = false;
        if (linkedDoor != null)
            linkedDoor.OpenDoor();
    }
}
