using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBtn : MonoBehaviour
{
    public GameObject MainUI;
    public Button[] Btn;

    public void Onstart() 
    {
        MainUI.SetActive(false);
    }

    public void OnOption() 
    {
        
    }

    public void OnExit() 
    { 
    
    }
}
