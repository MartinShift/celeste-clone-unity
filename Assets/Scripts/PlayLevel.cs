using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayLevel : MonoBehaviour, IPointerClickHandler
{
    public int Level = 1;

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayGame.CurrentLevel = Level;
        PlayGame.LastCheckpoint = null;
        PlayerControls.LastCheckpoint = null;
        PlayGame.Save();
        SceneManager.LoadScene(Level);
    }
}
