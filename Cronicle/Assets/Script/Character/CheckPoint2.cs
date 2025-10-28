using UnityEngine;

public class Stage2 : MonoBehaviour
{
    public Transform respawnPoint;

    void Start()
    {
        StageManager.Instance.SetRespawnPoint(respawnPoint);
    }
}
