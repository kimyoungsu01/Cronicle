using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class View : MonoBehaviour
{
    public Transform playerTransform; // 캐릭터 위치
    public Cinemachine.CinemachineConfiner view1; // 카메라
    public Collider stage1; // 스테이지1


    private void Update()
    {
        //player.layer = 
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerTransform.CompareTag("player"))
        {
            // player를 따라간다.
            stage1.enabled = false;
            //view1.m_BoundingVolume.enabled = false;
        }
    }
}
