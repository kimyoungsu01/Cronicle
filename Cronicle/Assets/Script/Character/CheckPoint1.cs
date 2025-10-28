using UnityEngine;

public class Stage1 : MonoBehaviour
{
    public Transform respawnPoint;

    void Start()
    {
        StageManager.Instance.SetRespawnPoint(respawnPoint);
    }
}
