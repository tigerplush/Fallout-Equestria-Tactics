using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Action : ScriptableObject
{
    public abstract void Act();

    public virtual void DrawSceneGUI(SerializedObject serializedObject)
    {

    }
}
