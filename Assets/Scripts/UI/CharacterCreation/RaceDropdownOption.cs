using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaceDropdownOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public RaceDropdown dropdown;

    public void OnPointerDown(PointerEventData eventData)
    {
        dropdown.Exit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        dropdown.Enter(transform.GetSiblingIndex());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dropdown.Exit();
    }
}
