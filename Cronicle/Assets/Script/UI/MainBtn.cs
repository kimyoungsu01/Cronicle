using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBtn : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject optionPanel;
    public GameObject gravityskill;

    public void Onstart() 
    {
        Debug.Log(MainUI);
        MainUI.SetActive(false);
        gravityskill.SetActive(true);
    }

    public void OnOption() 
    {
        optionPanel.SetActive(true);
    }

    public void OnExit() 
    {
       Debug.Log("Á¾·á");
       Application.Quit();
    }

    public void OnBack() 
    {
       optionPanel.SetActive(false);
    }
}
