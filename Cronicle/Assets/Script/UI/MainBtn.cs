using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MainBtn : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject optionPanel;
    public GameObject gravityskill;

    [SerializeField] private PlayerController playerController;

    public void Onstart() 
    {
        Debug.Log(MainUI);
        MainUI.SetActive(false);
        gravityskill.SetActive(true);
        playerController.OnStandUp();
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
