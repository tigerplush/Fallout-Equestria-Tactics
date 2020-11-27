using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();

    public List<Armor> equippedArmor = new List<Armor>();

    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;

    public delegate void EquipmentChangedHandler();
    public EquipmentChangedHandler EquipmentChanged;

    public void Add(Item item)
    {
        inventory.Add(item);
    }

    public void Remove(Item item)
    {
        inventory.Remove(item);
    }

    public void Equip(Item item, ItemData data)
    {
        item.Equip(this, data);

        EquipmentChanged?.Invoke();
    }

    public void Equip(Weapon weapon, WeaponType type)
    {
        switch(type)
        {
            case WeaponType.Primary:
                primaryWeapon = weapon;
                if (secondaryWeapon == weapon)
                {
                    secondaryWeapon = null;
                }
                break;
            case WeaponType.Secondary:
                secondaryWeapon = weapon;
                if (primaryWeapon == weapon)
                {
                    primaryWeapon = null;
                }
                break;
        }
    }

    public void Equip(Armor armor)
    {
        //Create copy of worn armor because you can't change an iterator
        Armor[] tempArmor = equippedArmor.ToArray();
        foreach(Armor am in tempArmor)
        {
            //if worn armor was already removed, skip check
            if(!equippedArmor.Contains(am))
            {
                continue;
            }
            //foreach bodypart in new armor, unequip the old one
            foreach(BodyPart part in armor.equippedAt)
            {
                if(am.EquippedAt(part))
                {
                    equippedArmor.Remove(am);
                }
            }
        }

        //equip new armor
        equippedArmor.Add(armor);
    }

    public void Equip(Armor armor, BodyPart bodyPart)
    {

    }

    public void Unequip(BodyPart bodyPart)
    {
        Armor[] tempArmor = equippedArmor.ToArray();
        foreach(Armor armor in tempArmor)
        {
            if(armor.EquippedAt(bodyPart))
            {
                equippedArmor.Remove(armor);
            }
        }

        EquipmentChanged?.Invoke();
    }

    public void Unequip(WeaponType type)
    {
        switch(type)
        {
            case WeaponType.Primary:
                primaryWeapon = null;
                break;
            case WeaponType.Secondary:
                secondaryWeapon = null;
                break;
        }

        EquipmentChanged?.Invoke();
    }

    public float GetArmorClass(BodyPart bodyPart)
    {
        Armor armor = equippedArmor.Find(am => am.EquippedAt(bodyPart));
        if(armor != null)
        {
            return armor.armorClass;
        }
        return 0f;
    }

    public float GetArmorClass()
    {
        float armorClass = 0f;
        foreach(Armor armor in equippedArmor)
        {
            armorClass += armor.armorClass;
        }
        return armorClass;
    }

    public float GetDamageThreshold(BodyPart bodyPart)
    {
        Armor armor = equippedArmor.Find(am => am.EquippedAt(bodyPart));
        if (armor != null)
        {
            return armor.damageThreshold;
        }
        return 0f;
    }

    public float GetDamageThreshold()
    {
        float damageThreshold = 0f;
        foreach (Armor armor in equippedArmor)
        {
            damageThreshold += armor.damageThreshold;
        }
        return damageThreshold;
    }

    public float GetDamageResistance(BodyPart bodyPart)
    {
        Armor armor = equippedArmor.Find(am => am.EquippedAt(bodyPart));
        if(armor != null)
        {
            return armor.damageResistance;
        }
        return 0f;
    }

    public float GetDamageResistance()
    {
        float damageResistance = 0f;
        foreach (Armor armor in equippedArmor)
        {
            damageResistance += armor.damageResistance;
        }
        return damageResistance;
    }
}
