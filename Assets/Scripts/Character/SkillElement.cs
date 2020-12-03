using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Skill Element", order = 9)]
public class SkillElement : Element, IComparable<SkillElement>
{
    public Formula defaultFormula;

    public int CompareTo(SkillElement other)
    {
        return 0;
    }
}
