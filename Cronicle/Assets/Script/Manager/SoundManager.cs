using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] AudioClip[] bgm;
    [SerializeField] AudioClip[] others;
    [SerializeField] AudioClip[] players;

    [SerializeField] AudioSource listenBgm;
    [SerializeField] AudioSource listenother;
    [SerializeField] AudioSource listenplayers;

    [Range(0f, 1f)] public float masterVolume;
    [Range(0f, 1f)] public float bgmVolume;
    [Range(0f, 1f)] public float mixVolume;

    string musicPath = "Sound/Music/";

    public static SoundManager instance { get; set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        others = Resources.LoadAll<AudioClip>(musicPath + "OtherSound");
        bgm = Resources.LoadAll<AudioClip>(musicPath + "BGMs");
        players = Resources.LoadAll<AudioClip>(musicPath + "Players");

        // 시작시 볼륨 값 고정
        masterVolume = 0.7f;
        bgmVolume = 0.5f;
        mixVolume = 0.5f;
    }
}
