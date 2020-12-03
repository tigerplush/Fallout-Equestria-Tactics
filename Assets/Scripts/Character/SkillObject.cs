using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Skill Object", order = 2)]
public class SkillObject : IntAttributeObject
{
    public Formula formula;

    public SkillObject()
    {
        minValue = 0;
        maxValue = 100;
        m_Value = 0;
    }

    public override int Value
    {
        get
        {
            int value = m_Value;
            if(formula != null && !float.IsNaN(formula.Value))
            {
                value += Mathf.FloorToInt(formula.Value);
            }
            return value;
        }
    }
}
