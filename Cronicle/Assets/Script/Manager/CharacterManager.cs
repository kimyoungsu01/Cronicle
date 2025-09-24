using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Player _player { get; set; }
    public PlayerController playerController { get; private set; }
    public static CharacterManager instance { get; set; }

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
