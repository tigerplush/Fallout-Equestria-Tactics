﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmorSlotUI : MonoBehaviour, ISlot
{
    public TextMeshProUGUI text;
    public Button button;
    public BodyPart acceptedBodypart;

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

    private bool TestAcceptability(Item item)
    {
        Armor armor = item as Armor;
        if (armor != null)
        {
            if (armor.equippedAt.Contains(acceptedBodypart))
            {
                return true;
            }
        }
        return false;
    }

    public void Reset()
    {
        button.interactable = true;
    }

    public bool Accept(Item item, out ItemData data)
    {
        data = new ItemData();
        return TestAcceptability(item);
    }

    public void Set(Armor armor)
    {
        text.text = "";
        if(armor != null)
        {
            text.text = armor.name;
        }
    }

    public void Unequip()
    {
        inventoryUI.Unequip(acceptedBodypart);
    }
}
