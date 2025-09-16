using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "Puzzle/HavyObsculer", order = 1)]
public class HavyObsculer : ScriptableObject
{
    public string obstacleName;
    public Rigidbody mass;
    public GameObject obstaclePrefabs;
    private void Awake()
    {
        Rigidbody _rigidbody = mass.GetComponent<Rigidbody>();
        _rigidbody.mass = 100f;
    }

    public void massForce()
    {
        
    }
}


