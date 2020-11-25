using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    public RectTransform content;
    public GameObject itemUiPrefab;

    public Sprite tempSprite;

    public ArmorSlotUI[] armorSlots;

    private List<ItemUI> itemUiPool = new List<ItemUI>();
    private GameObject draggableIcon;
    private Inventory inventory;

    private void Awake()
    {
        foreach (ArmorSlotUI slot in armorSlots)
        {
            slot.SetUp(this);
        }
    }

    private void OnEnable()
    {
        Character currentCharacter = BattleManager.instance.CurrentCharacter;
        
        UpdateUI(currentCharacter.inventory);
    }

    public void UpdateUI(Inventory inventory)
    {
        this.inventory = inventory;
        int numberOfItems = inventory.inventory.Count;

        //Create missing number of prefabs
        for (int i = itemUiPool.Count; i < numberOfItems; i++)
        {
            GameObject itemUiObject = Instantiate<GameObject>(itemUiPrefab, content);
            ItemUI itemUi = itemUiObject.GetComponent<ItemUI>();
            itemUi.Init(this);
            itemUiPool.Add(itemUi);
        }

        //enable only as much itemUis as items exist
        for (int i = 0; i < itemUiPool.Count; i++)
        {
            itemUiPool[i].gameObject.SetActive(i < numberOfItems);
            itemUiPool[i].Set(inventory.inventory[i]);
        }

        foreach(ArmorSlotUI slotUi in armorSlots)
        {
            ArmorSlot armorSlot = inventory.armorSlots.Find(slot => slot.bodyPart == slotUi.acceptedBodypart);
            if(armorSlot != null)
            {
                slotUi.Set(armorSlot.armor);
            }
        }
    }

    public void Hover(Item item)
    {
    }

    public void BeginDrag(Item item, PointerEventData eventData)
    {

        //disable all slots that can't hold the item
        foreach(ArmorSlotUI slot in armorSlots)
        {
            slot.CanAccept(item);
        }

        draggableIcon = new GameObject("icon");
        draggableIcon.transform.SetParent(transform, false);
        draggableIcon.transform.SetAsLastSibling();

        Image image = draggableIcon.AddComponent<Image>();
        image.sprite = tempSprite;
        image.SetNativeSize();

        MoveIcon(eventData);
    }

    public void Drag(PointerEventData eventData)
    {
        if(draggableIcon != null)
        {
            MoveIcon(eventData);
        }
    }

    public void EndDrag(Item item, PointerEventData eventData)
    {
        if(draggableIcon != null)
        {
            Destroy(draggableIcon);
        }

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);
        if(results.Count > 0)
        {
            ISlot slot = results[0].gameObject.GetComponent<ISlot>();
            if(slot != null)
            {
                if(slot.Accept(item) && inventory != null)
                {
                    inventory.Equip(item);
                }

            }
        }

        //make all slots available again
        foreach(ArmorSlotUI slot in armorSlots)
        {
            slot.Reset();
        }
    }

    private void MoveIcon(PointerEventData eventData)
    {
        var rt = draggableIcon.transform as RectTransform;
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
        }
    }

    public void Unequip(BodyPart part)
    {
        inventory.Unequip(part);
    }
}
