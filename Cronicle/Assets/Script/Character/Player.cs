using UnityEngine;

public class Player : MonoBehaviour
{
  public PlayerController controller;

    private void Awake() 
    {
        //CharacterManager.instance._player = this;
        Debug.Log(this);
        controller = GetComponent<PlayerController>();
    }
}
