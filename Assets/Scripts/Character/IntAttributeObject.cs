using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/New Int Attribute")]
public class IntAttributeObject : AttributeObject<int>
{
    public override int Value
    {
        get
        {
            return m_Value;
        }
        set
        {
            m_Value = Mathf.Clamp(value, minValue, maxValue);
            ValueChanged?.Invoke();
        }
    }

    public IntAttributeObject()
    {
        minValue = 1;
        maxValue = 10;
        Value = 5;
    }

    public override bool Increase(int increment)
    {
        if (Value + increment <= maxValue)
        {
            Value += increment;
            return true;
        }
        return false;
    }

    public override bool Decrease(int decrement)
    {
        if(Value - decrement >= minValue)
        {
            Value -= decrement;
            return true;
        }
        return false;
    }
}
