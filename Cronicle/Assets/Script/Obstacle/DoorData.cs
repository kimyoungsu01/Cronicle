using UnityEngine;


public class DoorData : MonoBehaviour
{
    // Door Open Model
    [Header("Door Open")]
    public string doorName;
    public Animator animator;
    public bool isOpens;

    public void Awake()
    {
        doorName = transform.name;
        animator = GetComponent<Animator>();
        isOpens = false;
    }
}
