using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI;

[CustomEditor(typeof(Action), true)]
public class ActionEditor : Editor
{
    Action action;
    SerializedProperty waypoints;
    private void OnEnable()
    {
        action = target as Action;
        waypoints = serializedObject.FindProperty("waypoints");
        SceneView.duringSceneGui += OnSceneGUI;
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        EditorGUI.BeginChangeCheck();
        action.DrawSceneGUI(serializedObject);
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }

    public override void OnInspectorGUI()
    {
        for (int i = 0; i < waypoints.arraySize; i++)
        {
            SerializedProperty waypoint = waypoints.GetArrayElementAtIndex(i);
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(waypoint);
            //waypoint.vector3Value = EditorGUILayout.Vector3Field($"{i}", waypoint.vector3Value);
            if (GUILayout.Button("+", GUILayout.Width(20f)))
            {
                waypoints.InsertArrayElementAtIndex(i);
            }
            if (GUILayout.Button("-", GUILayout.Width(20f)))
            {
                waypoints.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.EndHorizontal();
            if(EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        if(GUILayout.Button("Add waypoint") && waypoints != null)
        {
            waypoints.arraySize++;
            serializedObject.ApplyModifiedProperties();
        }
    }
}
