using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    public DoorData[] doorDatas;

    private Dictionary<string, DoorData> doorData = new Dictionary<string, DoorData>();
    
    private void start()
    {
        foreach (DoorData door in doorDatas) { doorData.Add(door.name,door); }
    }

    public void Update()
    {
        OnOpenDoor();
    }

    public void OnOpenDoor()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //DoorOpen("3번째문", true);
        }
    }
}


