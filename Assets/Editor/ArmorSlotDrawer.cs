using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ArmorSlot))]
public class ArmorSlotDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        Rect bodyPartRect = new Rect(position.x, position.y, position.width / 2f, position.height);
        Rect armorRect = new Rect(position.x + position.width / 2f, position.y, position.width / 2f, position.height);

        EditorGUI.PropertyField(bodyPartRect, property.FindPropertyRelative("bodyPart"), GUIContent.none);
        EditorGUI.PropertyField(armorRect, property.FindPropertyRelative("armor"), GUIContent.none);
        EditorGUI.EndProperty();
    }
}
