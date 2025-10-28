using UnityEngine;

public class Stage4 : MonoBehaviour
{
    public Transform respawnPoint;

    void Start()
    {
        StageManager.Instance.SetRespawnPoint(respawnPoint);
    }
}
