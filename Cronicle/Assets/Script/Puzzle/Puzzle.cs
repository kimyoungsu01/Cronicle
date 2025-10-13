using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class Puzzle : MonoBehaviour
{
    [Header("Valve")]
    public GameObject valvePrefab;
    public float valveZSpeed;
    public bool valveCheck;

    public GameObject[] puzzle;
    
    //private Quaternion raberRotation;
    private Vector3 raberPush,raberDrop = Vector3.down;

    [Header("Door")]
    public GameObject[] door;
    public HingeJoint hinge;

    private void Start()
    {
        StartCoroutine(PuzzleSquence());
    }

    IEnumerator PuzzleSquence()
    {
        yield return StartCoroutine(RotationValue());
        yield return StartCoroutine(OpenDoor());
    }

    // 코루틴 && 델타 타임 이용해서 천천히 회전
    IEnumerator RotationValue() 
    {
        // 벨브 회전
        float valveRotated = 0f;
        
        while (valveRotated < 45f)
        {
            float step = valveZSpeed * Time.deltaTime; // 델타 타임을 곱하여 프레임 독립적인 속도 조절
            puzzle[0].transform.Rotate(0, 0, step); // Z축으로 회전
            valveRotated += step; // 회전된 각도 누적
            yield return null; 
        }
    }

    //// 레버 회전
    //float laverRotated = 0f;

    //while (laverRotated < 180f)
    //{
    //    yield return null;
    //}

    //// 버튼 push
    //float buttonPosited = 0f;

    //while (buttonPosited < 180f)
    //{
    //    yield return null;
    //}

    IEnumerator OpenDoor() 
    {
        // 문 열림
        hinge = door[0].GetComponent<HingeJoint>();
        JointLimits limits = hinge.limits;
        hinge.useLimits = true;

        float curMax = limits.max;
        float targetMin = -90f; // 문이 완전히 닫혔을 때의 최소 각도
        float targetMax = 120f; // 문이 완전히 열렸을 때의 최대 각도
        float doorRotated = 0f;

        while (doorRotated < 1f)
        {
            doorRotated += Time.deltaTime;
            float angle = Mathf.Lerp(curMax, targetMax, doorRotated);
            door[0].transform.rotation = Quaternion.Euler(0,0,angle); // 현재 z축 회전값에 더함
            hinge.limits = limits;
            yield return null;
        }
    }
}
