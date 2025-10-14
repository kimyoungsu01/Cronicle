using UnityEngine;
using UnityEngine.UI;
using TMPro; // TMP를 쓴다면 필요

public class ResolutionOptionUI : MonoBehaviour
{
    [Header("UI 연결")]
    public Button btnLeft;
    public Button btnRight;
    public TMP_Text resolutionText; // Text를 쓴다면 Text로 변경

    [Header("해상도 목록")]
    public Vector2Int[] resolutions = new Vector2Int[]
    {
        new Vector2Int(1280, 720),
        new Vector2Int(1920, 1080),
        new Vector2Int(2560, 1440),
        new Vector2Int(3840, 2160)
    };

    private int currentIndex = 0;

    void Start()
    {
        btnLeft.onClick.AddListener(OnLeftClick);
        btnRight.onClick.AddListener(OnRightClick);
        UpdateResolutionText();
    }

    void OnLeftClick()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = resolutions.Length - 1; // 순환식

        ApplyResolution();
    }

    void OnRightClick()
    {
        currentIndex++;
        if (currentIndex >= resolutions.Length)
            currentIndex = 0;

        ApplyResolution();
    }

    void ApplyResolution()
    {
        Vector2Int res = resolutions[currentIndex];
        resolutionText.text = $"{res.x} x {res.y}";

        // 실제 해상도 변경 (원한다면 주석 해제)
        // Screen.SetResolution(res.x, res.y, FullScreenMode.Windowed);

        Debug.Log($"해상도 변경: {res.x}x{res.y}");
    }

    void UpdateResolutionText()
    {
        Vector2Int res = resolutions[currentIndex];
        resolutionText.text = $"{res.x} x {res.y}";
    }
}
