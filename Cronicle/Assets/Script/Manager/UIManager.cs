using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public MainBtn mainBtn;
    public GameObject keybordUI;

    DOTweenAnimation dotweenAnimation;
    public static UIManager instance { get; set; }

    private void Awake()
    {
        TutorialUI();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TutorialUI() 
    {
        keybordUI.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
