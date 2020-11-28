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
    public WeaponSlotUI primary;
    public WeaponSlotUI secondary;

    private List<ItemUI> itemUiPool = new List<ItemUI>();
    private GameObject draggableIcon;
    private Inventory inventory;

    private void Awake()
    {
        foreach (ArmorSlotUI slot in armorSlots)
        {
            slot.SetUp(this);
        }
        primary.SetUp(this);
        secondary.SetUp(this);
    }

    private void OnEnable()
    {
        Character currentCharacter = BattleManager.instance.CurrentCharacter;
        
        UpdateUI(currentCharacter.inventory);
    }

    public void UpdateUI(Inventory inventory)
    {
        this.inventory = inventory;
        int numberOfItems = inventory.Items.Count;

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
            itemUiPool[i].Set(inventory.Items[i]);
        }

        foreach(ArmorSlotUI slotUi in armorSlots)
        {
            Armor am = inventory.equippedArmor.Find(armor => armor.EquippedAt(slotUi.acceptedBodypart));
            slotUi.Set(am);
        }

        primary.Set(inventory.primaryWeapon);
        secondary.Set(inventory.secondaryWeapon);
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
        primary.CanAccept(item);
        secondary.CanAccept(item);

        //create drag icon
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
                ItemData data;
                if (slot.Accept(item, out data) && inventory != null)
                {
                    inventory.Equip(item, data);
                }
            }
        }

        //make all slots available again
        foreach(ArmorSlotUI slot in armorSlots)
        {
            slot.Reset();
        }
        primary.Enable();
        secondary.Enable();
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

    public void Unequip(WeaponType type)
    {
        inventory.Unequip(type);
    }
}
