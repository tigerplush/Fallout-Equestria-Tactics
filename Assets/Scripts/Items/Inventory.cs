using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();

    public Armor headArmor;
    public Armor faceArmor;
    public Armor chestArmor;
    public Armor leftArmArmor;
    public Armor rightArmArmor;
    public Armor leftLegArmor;
    public Armor rightLegArmor;

    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;

    public void Add(Item item)
    {
        inventory.Add(item);
    }

    public void Remove(Item item)
    {
        inventory.Remove(item);
    }

    public void Equip(Weapon weapon)
    {

    }

    public void Equip(Armor armor)
    {

    }
}
