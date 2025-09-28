using JBooth.MicroVerseCore;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidioOption : MonoBehaviour
{
    public Dropdown resoiutionDropdown;
    List<Resolution> resolutions = new List<Resolution>();
    private void Start()
    {
        InitUI();
    }

    void InitUI() 
    {
        resolutions.AddRange(Screen.resolutions);
        resoiutionDropdown.options.Clear();

        foreach (Resolution option in resolutions)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData();
            optionData.text = option.width + "x" + option.height + "";
            resoiutionDropdown.options.Add(optionData);
        }
        resoiutionDropdown.RefreshShownValue();
    }
}
