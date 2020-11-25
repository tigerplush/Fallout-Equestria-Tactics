using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();

    public List<ArmorSlot> armorSlots;

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

    public void Equip(Item item)
    {
        item.Equip(this);
        UIManager.instance.inventoryUI.UpdateUI(this);
    }

    public void Equip(Weapon weapon)
    {

    }

    public void Equip(Armor armor)
    {

        // check if character has body part to equip armor
        foreach(BodyPart part in armor.equippedAt)
        {
            if(!armorSlots.Exists(slot => slot.bodyPart == part))
            {
                return;
            }
        }

        // unequip old armor
        foreach (BodyPart part in armor.equippedAt)
        {
            ArmorSlot armorSlot = armorSlots.Find(slot => slot.bodyPart == part);
            if(armorSlot != null && armorSlot.armor)
            {
                Unequip(armorSlot.armor);
            }
        }

        // equip new armor
        foreach(BodyPart part in armor.equippedAt)
        {
            ArmorSlot armorSlot = armorSlots.Find(slot => slot.bodyPart == part);
            if (armorSlot != null)
            {
                armorSlot.armor = armor;
            }
        }
    }

    public void Unequip(Armor armor)
    {
        foreach(BodyPart part in armor.equippedAt)
        {
            ArmorSlot armorSlot = armorSlots.Find(slot => slot.bodyPart == part);
            if(armorSlot != null)
            {
                armorSlot.armor = null;
            }
        }
        UIManager.instance.inventoryUI.UpdateUI(this);
    }

    public void Unequip(BodyPart part)
    {
        if(armorSlots.Exists(slot => slot.bodyPart == part && slot.armor != null))
        {
            ArmorSlot armorSlot = armorSlots.Find(slot => slot.bodyPart == part);
            Unequip(armorSlot.armor);
        }
    }
}
