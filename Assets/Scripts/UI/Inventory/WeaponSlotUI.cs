using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSlotUI : MonoBehaviour, ISlot
{
    public Button button;
    public TextMeshProUGUI text;
    public WeaponType type;

    private InventoryUI inventoryUI;

    public void SetUp(InventoryUI inventoryUI)
    {
        this.inventoryUI = inventoryUI;
    }

    public bool CanAccept(Item item)
    {
        bool acceptability = TestAcceptability(item);
        button.interactable = acceptability;
        return acceptability;
    }

    public bool Accept(Item item, out ItemData data)
    {
        data = new WeaponData(type);
        return TestAcceptability(item);
    }

    public bool TestAcceptability(Item item)
    {
        Weapon weapon = item as Weapon;
        return weapon != null;
    }

    public void Reset()
    {
        button.interactable = true;
    }

    public void Set(Weapon weapon)
    {
        text.text = "";
        if (weapon != null)
        {
            text.text = weapon.name;
        }
    }

    public void Unequip()
    {
        inventoryUI.Unequip(type);
    }
}
