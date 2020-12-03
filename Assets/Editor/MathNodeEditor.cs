using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(MathNode))]
public class MathNodeEditor : NodeEditor
{
    public override Color GetTint()
    {
        MathNode node = target as MathNode;
        Formula formula = node.graph as Formula;
        if(node == formula.output)
        {
            return new Color(178f / 255f, 24f / 255f, 34f / 255f);
        }
        return base.GetTint();
    }
}
