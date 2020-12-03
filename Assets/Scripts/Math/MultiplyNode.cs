using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class MultiplyNode : MathNode
{
    [Input] public float a;
    [Input] public float b;
    [Output] public float product;
    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    // Return the correct value of an output port when requested
    public override object GetValue(NodePort port)
    {
        float a = GetInputValue<float>("a", this.a);
        float b = GetInputValue<float>("b", this.b);
        return a * b; // Replace this
    }
}
