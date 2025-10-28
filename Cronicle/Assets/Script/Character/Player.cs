using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
  public PlayerController controller;

    private void Awake() 
    {
        CharacterManager.instance.Player = this;
        controller = GetComponent<PlayerController>();
    }
}
