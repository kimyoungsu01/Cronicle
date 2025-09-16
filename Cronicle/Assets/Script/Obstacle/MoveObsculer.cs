using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "Puzzle/MoveObsculer", order = 1)]
public class MoveObsculer : ScriptableObject
{
    public string obstacleName;
    public float damage;
    public GameObject obstaclePrefabs;
}
