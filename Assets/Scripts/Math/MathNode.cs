using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class MathNode : Node
{
    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    // Return the correct value of an output port when requested
    public override object GetValue(NodePort port)
    {
        return null; // Replace this
    }

    [ContextMenu("Set as Output")]
    public void SetAsOutput()
    {
        Formula formula = graph as Formula;
        if(formula != null)
        {
            formula.SetOutput(this);
        }
    }
}