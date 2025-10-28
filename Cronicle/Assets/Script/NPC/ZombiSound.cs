using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Transform player;               // 플레이어 Transform (Inspector에서 연결)
    public float detectRange = 15f;        // 인식 거리
    public float minVolume = 0.1f;         // 최소 볼륨
    public float maxVolume = 1.0f;         // 최대 볼륨
    public float growlInterval = 5f;       // 좀비 소리 주기 (초 단위)

    private AudioSource zombieSource;      // 좀비 전용 AudioSource
    private float growlTimer = 0f;

    void Start()
    {
        zombieSource = gameObject.AddComponent<AudioSource>();
        zombieSource.clip = SoundManager.Instance.zombi;
        zombieSource.loop = false;
        zombieSource.playOnAwake = false;
    }

    void Update()
    {
        if (player == null) return;

        // 거리 계산
        float distance = Vector3.Distance(transform.position, player.position);

        // 거리 기반 볼륨 계산
        float volume = Mathf.Lerp(maxVolume, minVolume, distance / detectRange);
        volume = Mathf.Clamp(volume, minVolume, maxVolume);

        if (distance < detectRange)
        {
            growlTimer += Time.deltaTime;
            if (growlTimer >= growlInterval)
            {
                zombieSource.volume = volume * SoundManager.Instance.sfxVolume;
                zombieSource.Play();
                growlTimer = 0f;
            }
        }
    }
}
