using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionData : ItemData
{

}

[CreateAssetMenu(menuName = "Items/Ammunition", order = 18)]
public class Ammunition : Item
{
    public AmmunitionElement type;

    public override void Equip(Inventory inventory, ItemData data)
    {
    }
}
