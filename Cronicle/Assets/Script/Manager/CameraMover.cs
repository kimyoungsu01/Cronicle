using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraMover : MonoBehaviour
{
    public CinemachineVirtualCamera stageCamera; // 카메라

    public Transform[] stagePoints; // 스테이지 위치 배열

    public float moveDuration = 2.0f; // 카메라 이동 시간

    public int currentStage = 0; // 현재 스테이지
    public Transform camTransform; // 카메라 위치

    private void Start()
    {
        camTransform = stageCamera.transform;
        camTransform.position = stagePoints[0].position;
    }

    public void NextStage()
    {
        if (currentStage < stagePoints.Length -1) 
        {
            currentStage++;
            MoveCamera(stagePoints[currentStage]);
        }
    }

    public void PreviousStage()
    {
        if (currentStage > 0) 
        {
            currentStage--;
            MoveCamera(stagePoints[currentStage]);
        }
    }

    public void MoveCamera(Transform target) 
    {
       camTransform.DOMove(target.position, moveDuration).SetEase(Ease.InOutSine);
    }
}
