using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public float weight;
    public float value;

    public abstract void Equip(Inventory inventory);
}
