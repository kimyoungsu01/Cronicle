using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public ValvePuzzle valvePuzzle;
    public LeverPuzzle leverPuzzle;
    public ButtonPuzzle buttonPuzzle;

    void Update()
    {
        if (valvePuzzle.isCompleted && leverPuzzle.isCompleted && buttonPuzzle.isCompleted)
        {
            Debug.Log("모든 퍼즐 완료!");
        }
    }
}
