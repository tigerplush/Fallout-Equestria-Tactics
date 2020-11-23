using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WalkableSurface : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool walkable = true;
    public bool flyable = true;
    public float cost = 1f;
    public int maximumHeight = 2;

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 worldPosition = eventData.pointerCurrentRaycast.worldPosition;
        BattleManager.instance.ClickedSurface(worldPosition);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HexPointer.instance.Enable();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HexPointer.instance.Disable();
    }
}
