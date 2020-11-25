using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TextMeshProUGUI text;
    private InventoryUI inventoryUI;
    private Item item;

    public void Init(InventoryUI inventoryUI)
    {
        this.inventoryUI = inventoryUI;
    }

    public void Set(Item item)
    {
        this.item = item;
        text.text = item.name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(inventoryUI != null && item != null)
        {
            inventoryUI.Hover(item);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        inventoryUI.BeginDrag(item, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventoryUI.Drag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inventoryUI.EndDrag(item, eventData);
    }
}
