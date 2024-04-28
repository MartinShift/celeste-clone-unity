using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSwitch : MonoBehaviour, IPointerClickHandler
{
    public GameObject MainMenu; 
    public GameObject OptionsMenu; 

    public void Toggle()
    {
        if (MainMenu.activeSelf)
        {
            MainMenu.SetActive(false);
            OptionsMenu.SetActive(true);
        }
        else
        {
            MainMenu.SetActive(true);
            OptionsMenu.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }
}
