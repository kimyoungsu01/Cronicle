using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [Header("현재 스테이지의 부활 위치")]
    public Transform currentRespawnPoint;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // 스테이지가 바뀔 때 호출
    public void SetRespawnPoint(Transform newPoint)
    {
        currentRespawnPoint = newPoint;
        Debug.Log($"🏁 현재 스테이지 부활 위치 변경: {newPoint.name}");
    }

    // 현재 부활 위치 반환
    public Transform GetRespawnPoint()
    {
        return currentRespawnPoint;
    }
}
