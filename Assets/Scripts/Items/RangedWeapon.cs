using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Ranged Weapon", order = 12)]
public class RangedWeapon : Weapon
{
    public AmmunitionElement[] acceptedAmmunition;
    public int ammoCapacity;
}
