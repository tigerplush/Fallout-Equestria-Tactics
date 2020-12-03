using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class AttributeNode : MathNode
{
    public AttributeElement attribute;

    [Output] public float value;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    // Return the correct value of an output port when requested
    public override object GetValue(NodePort port)
    {
        Formula formula = graph as Formula;
        if (attribute != null && formula != null && formula.character != null && formula.character.attributes.Has(attribute))
        {
            return (float)formula.character.attributes.Get(attribute).Value;
        }
        return null;
    }
}
