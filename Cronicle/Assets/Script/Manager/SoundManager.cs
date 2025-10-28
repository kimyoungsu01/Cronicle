using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public static SoundManager Instance => instance;

    [Header("오디오 소스")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;  // 단발용
    private AudioSource loopSource; // 루프 전용
    private Coroutine fadeCoroutine; // 페이드 중복 방지용

    [Header("사운드 클립")]
    public AudioClip lagedoor;
    public AudioClip smalldoor;
    public AudioClip gun;
    public AudioClip jump;
    public AudioClip run;
    public AudioClip walk;
    public AudioClip zombi;

    [Range(0, 1)] public float bgmVolume = 1f;
    [Range(0, 1)] public float sfxVolume = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // 루프 전용 소스 생성
            loopSource = gameObject.AddComponent<AudioSource>();
            loopSource.loop = true;
            loopSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ===== BGM =====
    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;
        bgmSource.clip = clip;
        bgmSource.volume = bgmVolume;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource == null) return;
        bgmSource.Stop();
        bgmSource.clip = null;
    }

    // ===== 단발형 SFX =====
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("[SoundManager] 재생할 SFX가 비어있습니다!");
            return;
        }

        if (sfxSource == null)
        {
            Debug.LogError("[SoundManager] SFX AudioSource가 비어있습니다!");
            return;
        }

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    // ===== 루프형 SFX =====
    public void PlaySFXLoop(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("[SoundManager] 루프할 SFX가 비어있습니다!");
            return;
        }

        if (loopSource.isPlaying && loopSource.clip == clip)
            return; // 이미 같은 사운드 재생 중

        loopSource.clip = clip;
        loopSource.volume = sfxVolume;
        loopSource.Play();
    }

    public void StopSFXLoop()
    {
        if (loopSource.isPlaying)
            loopSource.Stop();
    }

    // ===== 볼륨 조절 =====
    public void SetBGMVolume(float value)
    {
        bgmVolume = value;
        if (bgmSource != null) bgmSource.volume = bgmVolume;
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        if (sfxSource != null) sfxSource.volume = sfxVolume;
        if (loopSource != null) loopSource.volume = sfxVolume;
    }

    // ===== 루프 SFX 교체 (Fade 효과 추가) =====
    public void FadeToLoop(AudioClip newClip, float fadeDuration = 0.5f)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeLoopCoroutine(newClip, fadeDuration));
    }

    private IEnumerator FadeLoopCoroutine(AudioClip newClip, float fadeDuration)
    {
        // 같은 사운드면 무시
        if (loopSource.clip == newClip && loopSource.isPlaying)
            yield break;

        float startVolume = loopSource.volume;

        // 기존 사운드 페이드 아웃
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            loopSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        loopSource.Stop();

        // 새로운 사운드로 교체
        loopSource.clip = newClip;
        if (newClip != null)
        {
            loopSource.Play();

            // 페이드 인
            t = 0f;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                loopSource.volume = Mathf.Lerp(0f, sfxVolume, t / fadeDuration);
                yield return null;
            }
        }

        fadeCoroutine = null;

        
    }
}
