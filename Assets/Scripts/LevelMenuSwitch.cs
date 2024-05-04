using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelMenuSwitch : MonoBehaviour, IPointerClickHandler
{
    public GameObject MainMenu;
    public GameObject LevelsMenu;
    public GameObject levelButtonPrefab;
    public GameObject container;
    public void Toggle()
    {
        if (MainMenu.activeSelf)
        {
            MainMenu.SetActive(false);
            LevelsMenu.SetActive(true);
            var levels = container.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var level in levels)
            {
                PlayLevel script = level.GetComponent<PlayLevel>();
                level.enabled = script.Level <= PlayGame.MaxLevel;
            }
        }
        else
        {
            MainMenu.SetActive(true);
            LevelsMenu.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }
}
