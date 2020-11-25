using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSlotUI : MonoBehaviour, ISlot
{
    public BodyPart acceptedBodypart;

    public bool CanAccept(Item item)
    {
        return false;
    }

    public void Accept(Item item)
    {
    }
}
