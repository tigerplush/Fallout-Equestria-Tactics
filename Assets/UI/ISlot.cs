using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot
{
    bool CanAccept(Item item);
    void Accept(Item item);
}
