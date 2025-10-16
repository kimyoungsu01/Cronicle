using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    [Header("퍼즐 설정")]
    public bool isCompleted = false;
    public DoorController linkedDoor;
    public Transform player;
    public float interactDistance = 2f;

    [Header("버튼 이동 관련")]
    public float pressDepth = 0.1f;   // 얼마나 눌리는지
    public float pressSpeed = 0.2f;
    private Vector3 startPos;
    private bool isPressing = false;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        if (isCompleted) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= interactDistance)
                StartCoroutine(PressButton());
        }
    }

    System.Collections.IEnumerator PressButton()
    {
        if (isPressing) yield break;
        isPressing = true;

        Vector3 targetPos = startPos - Vector3.up * pressDepth;

        // 눌림
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * (pressSpeed * 10);
            transform.localPosition = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        // 완료 처리
        CompletePuzzle();

        // 살짝 원위치
        t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * (pressSpeed * 10);
            transform.localPosition = Vector3.Lerp(targetPos, startPos, t);
            yield return null;
        }

        isPressing = false;
    }

    void CompletePuzzle()
    {
        isCompleted = true;
        Debug.Log($"{name} 버튼 퍼즐 완료!");

        if (linkedDoor != null)
            linkedDoor.OpenDoor();
    }
}
