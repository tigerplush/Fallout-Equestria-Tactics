using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{

}

public abstract class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public float weight;
    public float value;

    public abstract void Equip(Inventory inventory, ItemData data);
}
