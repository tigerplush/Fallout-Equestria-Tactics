using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SkillIndex))]
public class SkillIndexDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedObject skillIndex = new SerializedObject(property.objectReferenceValue);
        SerializedProperty skillElements = skillIndex.FindProperty("indexElements");
        SerializedProperty skillValues = skillIndex.FindProperty("indexValues");

        EditorGUI.BeginProperty(position, label, property);
        EditorGUILayout.PropertyField(skillElements);
        if (GUILayout.Button("Create Skills"))
        {
            SkillIndex index = skillIndex.targetObject as SkillIndex;
            index.CreateAttributes();
            foreach (SkillObject skill in index.indexValues)
            {
                AssetDatabase.AddObjectToAsset(skill, skill.parent);
            }
            AssetDatabase.SaveAssets();
        }

        for (int i = 0; i < skillValues.arraySize; i++)
        {
            EditorGUILayout.PropertyField(skillValues.GetArrayElementAtIndex(i));
        }
        EditorGUI.EndProperty();

        if (skillIndex.hasModifiedProperties)
        {
            skillIndex.ApplyModifiedProperties();
        }
    }
}
