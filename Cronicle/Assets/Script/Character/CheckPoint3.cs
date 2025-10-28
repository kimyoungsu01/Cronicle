using UnityEngine;

public class Stage3 : MonoBehaviour
{
    public Transform respawnPoint;

    void Start()
    {
        StageManager.Instance.SetRespawnPoint(respawnPoint);
    }
}
