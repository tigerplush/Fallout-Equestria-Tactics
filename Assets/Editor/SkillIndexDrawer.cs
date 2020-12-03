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
        SerializedProperty elementsToCreate = skillIndex.FindProperty("elementsToCreate");
        SerializedProperty indexElements = skillIndex.FindProperty("indexElements");
        SerializedProperty skillValues = skillIndex.FindProperty("indexValues");

        EditorGUI.BeginProperty(position, label, property);
        EditorGUILayout.PropertyField(elementsToCreate);
        if (GUILayout.Button("Create Skills"))
        {
            SkillIndex index = skillIndex.targetObject as SkillIndex;
            SkillObject[] createdValues = index.CreateAttributes();
            foreach (SkillObject skill in createdValues)
            {
                AssetDatabase.AddObjectToAsset(skill, skill.parent);
            }
            AssetDatabase.SaveAssets();
        }

        for (int i = 0; i < skillValues.arraySize; i++)
        {
            SkillElement element = indexElements.GetArrayElementAtIndex(i).objectReferenceValue as SkillElement;
            EditorGUILayout.PropertyField(skillValues.GetArrayElementAtIndex(i), new GUIContent(element.name));
        }
        EditorGUI.EndProperty();

        if (skillIndex.hasModifiedProperties)
        {
            skillIndex.ApplyModifiedProperties();
        }
    }
}
