using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Unarmed,
    Melee,
    Ranged
}

public enum DamageType
{
    Normal,
    Explosive
}

public enum AttackType
{
    Single,
    Aimed,
    Burst,
    Swing,
    Punch
}

[System.Serializable]
public class Attack
{
    public AttackType attackType;
    public int actionPointCost;
    public float range;
}

public enum WeaponType
{
    Primary,
    Secondary
}

public class WeaponData : ItemData
{
    public WeaponType type;

    public WeaponData(WeaponType type)
    {
        this.type = type;
    }
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapons/New Weapon")]
public class Weapon : Item
{
    public SkillType skillType;
    public DamageType damageType;

    public float minDamage;
    public float maxDamage;

    public Attack[] attacks;

    public int handsRequired = 1;
    public int strengthRequired = 1;

    public override void Equip(Inventory inventory, ItemData data)
    {
        WeaponData weaponData = data as WeaponData;
        inventory.Equip(this, weaponData.type);
    }
}
