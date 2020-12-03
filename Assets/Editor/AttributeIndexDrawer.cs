﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AttributeIndex))]
public class AttributeIndexDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedObject attributeIndex = new SerializedObject(property.objectReferenceValue);
        SerializedProperty attributeElements = attributeIndex.FindProperty("elementsToCreate");
        SerializedProperty attributeValues = attributeIndex.FindProperty("indexValues");

        EditorGUI.BeginProperty(position, label, property);
        EditorGUILayout.PropertyField(attributeElements);
        if(GUILayout.Button("Create Attributes"))
        {
            AttributeIndex index = attributeIndex.targetObject as AttributeIndex;
            IntAttributeObject[] createdValues = index.CreateAttributes();
            foreach(IntAttributeObject attribute in createdValues)
            {
                AssetDatabase.AddObjectToAsset(attribute, attribute.parent);
            }
            AssetDatabase.SaveAssets();
        }
        for(int i = 0; i < attributeValues.arraySize; i++)
        {
            EditorGUILayout.PropertyField(attributeValues.GetArrayElementAtIndex(i));
        }
        EditorGUI.EndProperty();

        if(attributeIndex.hasModifiedProperties)
        {
            attributeIndex.ApplyModifiedProperties();
        }
    }
}
