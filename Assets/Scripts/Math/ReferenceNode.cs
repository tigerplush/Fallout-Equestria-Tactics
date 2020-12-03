using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ReferenceNode : MathNode
{
    public IntAttributeObject attribute;

    [Output] public float value;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    // Return the correct value of an output port when requested
    public override object GetValue(NodePort port)
    {
        if(attribute != null)
        {
            return (float)attribute.Value;
        }
        return null;
    }
}
