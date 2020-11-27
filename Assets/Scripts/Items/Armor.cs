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

public class ArmorData : ItemData
{
    public BodyPart bodyPart;
    public ArmorData(BodyPart bodyPart)
    {
        this.bodyPart = bodyPart;
    }
}

[CreateAssetMenu(menuName = "Items/Armor")]
public class Armor : Item
{
    public List<BodyPart> equippedAt;
    public bool eitherOr = false;

    //armor class makes it harder to hit
    public float armorClass;

    //damageFinal = max(DamageDR-DT, basicDamage * 0.2)
    public float damageThreshold;

    //damageDr = BasicDamage * (100 - min(DR, 85))/100
    public float damageResistance;

    public override void Equip(Inventory inventory, ItemData data)
    {
        Armor armor = this;
        ArmorData armorData = data as ArmorData;
        if(eitherOr && armorData != null)
        {
            Armor clone = Instantiate(this);
            clone.name = name;
            clone.equippedAt.RemoveAll(slots => slots != armorData.bodyPart);
            armor = clone;
        }
        inventory.Equip(armor);
    }

    public bool EquippedAt(BodyPart bodyPart)
    {
        return equippedAt.Contains(bodyPart);
    }
}
