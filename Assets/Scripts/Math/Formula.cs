﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class Formula : NodeGraph {
    public MathNode output;

    public void SetOutput(MathNode output)
    {
        this.output = output;
    }

    public float Value
    {
        get
        {
            if(output != null)
            {
                foreach (NodePort port in output.Ports)
                {
                    object obj = output.GetValue(port);
                    float value = (float)obj;
                    return value;
                }
            }
            return float.NaN;
        }
    }
}