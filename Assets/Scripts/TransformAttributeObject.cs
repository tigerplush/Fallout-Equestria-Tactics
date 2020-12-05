using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attribute Objects/Transform")]
public class TransformAttributeObject : AttributeObject<Transform>
{
    public override Transform Value
    {
        get
        {
            return m_Value;
        }
        set
        {
            m_Value = value;
            ValueChanged?.Invoke();
        }
    }

    public override bool Increase(Transform increment)
    {
        return false;
    }

    public override bool Decrease(Transform decrement)
    {
        return false;
    }
}
