using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterManager characterManager;
    public ObsculerManager obsculerManager;
    public UIManager uiManager;
    public GunManager Gun;
    public static GameManager instance { get; set; }

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
        if (uiManager.mainBtn)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
