using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;

    public void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;
        Debug.Log("문이 열림!");

        // 문 열림 효과음 재생
        SoundManager.Instance.PlaySFXLoop(SoundManager.Instance.lagedoor);
    }
}
