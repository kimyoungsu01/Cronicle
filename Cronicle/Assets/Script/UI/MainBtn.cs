using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MainBtn : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject optionPanel;
    public GameObject gravityskill;
    public GameObject GravityOption;
    public DOTweenAnimation dotween;

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

    public void OnLightMove() 
    {
        dotween.StartCoroutine(LightMove());
    }

    IEnumerator LightMove() 
    {
        //transform.DORotate(360,2f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

        yield return null;
    }
}
