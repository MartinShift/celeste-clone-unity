using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour, IPointerClickHandler
{
    public PlayerControls player;

    public void OnPointerClick(PointerEventData eventData)
    {
        player.SwitchPause();
    }
}
