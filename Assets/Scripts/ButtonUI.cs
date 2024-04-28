using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private TextMeshProUGUI tmp;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tmp.color = new Color(144f / 255f, 251f / 255f, 141f / 255f, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tmp.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tmp.color = Color.white;
    }
}
