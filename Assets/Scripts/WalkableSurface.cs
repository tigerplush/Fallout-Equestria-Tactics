using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WalkableSurface : MonoBehaviour, IPointerClickHandler
{
    public int cost = 1;
    public int maximumHeight = 2;

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 worldPosition = eventData.pointerCurrentRaycast.worldPosition;
        BattleManager.instance.ClickedSurface(worldPosition);
    }
}
