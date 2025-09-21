using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "Puzzle/MoveObsculer")]
public class MoveObsculer : ScriptableObject
{
    public string obstacleName;
    public GameObject obstaclePrefabs;

    [Header("이동 설정")]
    public float moveSpeed; // 이동 속도
}
