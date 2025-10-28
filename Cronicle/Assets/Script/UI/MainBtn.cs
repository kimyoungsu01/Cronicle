using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainBtn : MonoBehaviour
{
    [Header("슬라이더 참조")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    public AudioClip loopClip;  // 버튼 누르면 재생할 루프 오디오
    private bool isPlaying = false;

    public GameObject MainUI;
    public GameObject optionPanel;
    public GameObject gravityskill;

    [SerializeField] private PlayerController playerController;

    public void Onstart() 
    {
        Debug.Log(MainUI);
        MainUI.SetActive(false);
        gravityskill.SetActive(true);
        playerController.OnStandUp();
        SoundManager.Instance.StopBGM();
    }

    public void OnOption() 
    {
        optionPanel.SetActive(true);
    }

    private void Start()
    {
        // SoundManager가 아직 생성 안 됐을 수도 있으니 Start()에서 한 번 더 확인
        if (SoundManager.Instance == null)
        {
            Debug.LogWarning("SoundManager가 씬에 없습니다!");
            return;
        }

        // 초기값 세팅
        bgmSlider.value = SoundManager.Instance.bgmVolume;

        // 리스너 등록
        bgmSlider.onValueChanged.AddListener(value =>
        {
            SoundManager.Instance.SetBGMVolume(value);
        });
    }


    public void OnExit() 
    {
       Debug.Log("종료");
       Application.Quit();
    }

    public void OnBack() 
    {
       optionPanel.SetActive(false);
    }
}
