using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterManager characterManager;
    public ObsculerManager obsculerManager;
    public SoundManager soundManager;
    public UIManager uiManager;
    public GunManager Gun;
    public static GameManager instance { get; set; }
    public object cameraZoomController { get; private set; }

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
    }

    private void Start()
    {
        if (uiManager.mainBtn.enabled == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // ¿¹: StageManager.cs
    //void OnStageChanged(int stageIndex)
    //{
    //    cameraZoomController.SetStage(stageIndex);
    //}

}
