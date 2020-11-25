﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSlotUI : MonoBehaviour, ISlot
{
    public Button button;
    public BodyPart acceptedBodypart;

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

    public bool Accept(Item item)
    {
        return TestAcceptability(item);
    }
}
