using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "PluggableAI/New State")]
public class State : ScriptableObject
{
    public Action[] actions;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
    }

    public void DoActions(StateController controller)
    {
        for(int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    public void DrawSceneGUI(SerializedProperty serializedProperty)
    {
        for(int i = 0; i < actions.Length; i++)
        {
            SerializedObject actionObject = new SerializedObject(serializedProperty.GetArrayElementAtIndex(i).objectReferenceValue);
            actions[i].DrawSceneGUI(actionObject);
        }
    }
}
