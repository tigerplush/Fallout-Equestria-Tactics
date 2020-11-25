using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum BodyParts
{
    None = 0,
    Head = 1,
    Face = 2,
    Chest = 4,
    LeftArm = 8,
    RightArm = 16,
    LeftLeg = 32,
    RightLeg = 64
}

[CreateAssetMenu(menuName = "Items/Armor")]
public class Armor : Item
{
    public BodyParts equippedAt;

    //armor class makes it harder to hit
    public float armorClass;

    //damageFinal = max(DamageDR-DT, basicDamage * 0.2)
    public float damageThreshold;

    //damageDr = BasicDamage * (100 - min(DR, 85))/100
    public float damageResistance;
}
