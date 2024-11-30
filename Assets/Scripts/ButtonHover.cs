using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Color targetColor = new Color(0, 0, 0);
        eventData.pointerEnter.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = targetColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.pointerEnter.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
    }
}
