using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GunManager : MonoBehaviour
{
    // 총알 프리팹 배열
    public GameObject[] prefabs;
    // 총알 오브젝트 리스트
    List<GameObject>[] bulletList;
   
    public static GunManager instance;

    private void Awake()
    {
        instance = this;

        bulletList = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            bulletList[i] = new List<GameObject>();
        }
    }

    internal GameObject Get(int i , Vector3 position, Quaternion rotation)
    {
        GameObject select = null;

        foreach (GameObject item in bulletList[i]) 
        {
            if (!item.activeSelf) 
            { 
                select = item;
                select.transform.position = position;
                select.transform.rotation = rotation;
                select.SetActive(true);
                break;
            }
        }

        if (!select) 
        { 
            select = Instantiate(prefabs[i],position,rotation);
            bulletList[i].Add(select);
        }
        return select;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Animator _animation = GetComponent<Animator>();

        if (_animation != null)
        {
            _animation.SetBool("IsGravityScale", true);
        }

        else
        {
            _animation.SetBool("IsGravityScale", false);
        }
    }
}
