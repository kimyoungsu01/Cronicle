using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
  public PlayerController controller;

    private void Awake() 
    {
        CharacterManager.instance._player = this;
        Debug.Log(this);
        controller = GetComponent<PlayerController>();
    }
}
