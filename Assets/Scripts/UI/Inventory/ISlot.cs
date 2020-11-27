using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot
{
    bool CanAccept(Item item);
    bool Accept(Item item, out ItemData data);
}
