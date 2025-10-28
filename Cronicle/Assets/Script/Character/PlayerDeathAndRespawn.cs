using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathAndRespawn_FadeSimple : MonoBehaviour
{
    [Header("스테이지별 부활 위치 (Inspector에서 지정)")]
    public Transform[] respawnPoints;
    public int currentStage = 0;

    [Header("페이드 이미지 (UI Image)")]
    public Image fadeImage;

    private Rigidbody rb;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetFadeAlpha(0f); // 시작 시 화면 투명
        Respawn();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("DeathZone"))
        {
            Debug.Log("💀 DeathZone 충돌!");
            StartCoroutine(DieAndRespawn());
        }
    }

    IEnumerator DieAndRespawn()
    {
        isDead = true;

        // 물리 정지
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        // 페이드 아웃 (어두워짐)
        yield return StartCoroutine(Fade(0f, 1f));

        // 부활
        yield return new WaitForSeconds(0.3f);
        Respawn();

        // 페이드 인 (밝아짐)
        yield return StartCoroutine(Fade(1f, 0f));

        rb.isKinematic = false;
        isDead = false;
    }

    void Respawn()
    {
        if (respawnPoints.Length == 0)
        {
            Debug.LogWarning("⚠️ RespawnPoint가 없습니다!");
            return;
        }

        if (currentStage >= respawnPoints.Length)
            currentStage = respawnPoints.Length - 1;

        Transform spawn = respawnPoints[currentStage];
        transform.position = spawn.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Debug.Log($"✨ 부활 완료 (스테이지 {currentStage + 1}) 위치: {spawn.position}");
    }

    public void SetStage(int stageIndex)
    {
        currentStage = stageIndex;
        Debug.Log($"📍 스테이지 변경됨: {currentStage + 1}");
    }

    IEnumerator Fade(float start, float end)
    {
        float duration = 0.5f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            SetFadeAlpha(Mathf.Lerp(start, end, t));
            yield return null;
        }
        SetFadeAlpha(end);
    }

    void SetFadeAlpha(float alpha)
    {
        if (fadeImage == null) return;
        Color c = fadeImage.color;
        c.a = alpha;
        fadeImage.color = c;
    }
}
