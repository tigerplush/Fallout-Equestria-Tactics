using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIndex : Index<SkillElement, SkillObject>
{
    public float Value(SkillElement skill)
    {
        if(Has(skill))
        {
            float value = 0f;
            value += (float)Get(skill).Value;
            value += skill.defaultFormula.Val(parent);
            return value;
        }
        return float.NaN;
    }
}
