using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayGame : MonoBehaviour, IPointerClickHandler
{
    public static int MaxLevel = 1;
    public void OnPointerClick(PointerEventData eventData)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MaxLevel);
    }
}
