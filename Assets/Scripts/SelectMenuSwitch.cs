using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectMenuSwitch : MonoBehaviour, IPointerClickHandler
{
    public GameObject MainMenu;
    public GameObject PlayMenu;

    public void Toggle()
    {
        if (MainMenu.activeSelf)
        {
            MainMenu.SetActive(false);
            PlayMenu.SetActive(true);
        }
        else
        {
            MainMenu.SetActive(true);
            PlayMenu.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

}
