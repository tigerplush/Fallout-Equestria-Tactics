using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPart
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
    public List<BodyPart> equippedAt;

    //armor class makes it harder to hit
    public float armorClass;

    //damageFinal = max(DamageDR-DT, basicDamage * 0.2)
    public float damageThreshold;

    //damageDr = BasicDamage * (100 - min(DR, 85))/100
    public float damageResistance;

    public override void Equip(Inventory inventory)
    {
        inventory.Equip(this);
    }

    public bool EquippedAt(BodyPart bodyPart)
    {
        return equippedAt.Contains(bodyPart);
    }
}
