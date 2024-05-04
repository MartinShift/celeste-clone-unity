using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewGame : MonoBehaviour , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        PlayGame.MaxLevel = 1;
        PlayGame.CurrentLevel = 1;
        PlayGame.LastCheckpoint = null;
        PlayerControls.LastCheckpoint = null;
        PlayGame.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
