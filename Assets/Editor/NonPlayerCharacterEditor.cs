using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NonPlayerCharacter))]
public class NonPlayerCharacterEditor : Editor
{
    NonPlayerCharacter npc;
    private void OnEnable()
    {
        npc = target as NonPlayerCharacter;
    }
    private void OnSceneGUI()
    {
        if(npc.currentState != null)
        {
            SerializedProperty currentState = serializedObject.FindProperty("currentState");
            SerializedProperty actions = new SerializedObject(currentState.objectReferenceValue).FindProperty("actions");
            npc.currentState.DrawSceneGUI(actions);
        }
    }
}
