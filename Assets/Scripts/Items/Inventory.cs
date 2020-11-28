using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> items = new List<Item>();

    public List<Armor> equippedArmor = new List<Armor>();

    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;

    public delegate void EquipmentChangedHandler();
    public EquipmentChangedHandler EquipmentChanged;

    public List<Item> Items
    {
        get
        {
            List<Item> inventory = new List<Item>();
            inventory.AddRange(items);
            inventory.AddRange(equippedArmor);
            if(primaryWeapon != null)
            {
                inventory.Add(primaryWeapon);
            }
            if (secondaryWeapon != null)
            {
                inventory.Add(secondaryWeapon);
            }
            inventory.Sort((x, y) => string.Compare(x.itemName, y.itemName));
            return inventory;
        }
    }

    public bool IsEquipped(Item item)
    {
        return !items.Contains(item);
    }

    private void Start()
    {
        CreateCopies();
    }

    /// <summary>
    /// Creates a copy of every item in inventory so it can be changed at runtime individually
    /// </summary>
    private void CreateCopies()
    {
        for(int i = 0; i < items.Count; i++)
        {
            items[i] = Instantiate(items[i]);
        }
        for(int i = 0; i < equippedArmor.Count; i++)
        {
            equippedArmor[i] = Instantiate(equippedArmor[i]);
        }

        if(primaryWeapon != null)
        {
            primaryWeapon = Instantiate(primaryWeapon);
        }

        if (secondaryWeapon != null)
        {
            secondaryWeapon = Instantiate(secondaryWeapon);
        }
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void Equip(Item item, ItemData data)
    {
        item.Equip(this, data);

        EquipmentChanged?.Invoke();
    }

    public void Equip(Weapon weapon, WeaponType type)
    {
        // Check if there is a weapon in this slot already
        // if so, unequip it
        Unequip(type);

        // check if the weapon you want to equip is in your item pool
        // if not, it is equipped at another slot
        if(!items.Contains(weapon))
        {
            if(primaryWeapon == weapon)
            {
                Unequip(WeaponType.Primary);
            }
            if (secondaryWeapon == weapon)
            {
                Unequip(WeaponType.Primary);
            }
        }

        // equip weapon at slot
        switch (type)
        {
            case WeaponType.Primary:
                primaryWeapon = weapon;
                break;
            case WeaponType.Secondary:
                secondaryWeapon = weapon;
                break;
        }

        Remove(weapon);
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
                    if(equippedArmor.Remove(am))
                    {
                        Add(am);
                    }
                }
            }
        }

        //equip new armor
        equippedArmor.Add(armor);
        Remove(armor);
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
                if(equippedArmor.Remove(armor))
                {
                    Add(armor);
                }
            }
        }

        EquipmentChanged?.Invoke();
    }

    public void Unequip(WeaponType type)
    {
        switch(type)
        {
            case WeaponType.Primary:
                if(primaryWeapon != null)
                {
                    Add(primaryWeapon);
                }
                primaryWeapon = null;
                break;
            case WeaponType.Secondary:
                if (secondaryWeapon != null)
                {
                    Add(secondaryWeapon);
                }
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
