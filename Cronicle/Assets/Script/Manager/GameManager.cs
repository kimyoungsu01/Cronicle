using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterManager characterManager;
    public ObsculerManager obsculerManager;
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
}
