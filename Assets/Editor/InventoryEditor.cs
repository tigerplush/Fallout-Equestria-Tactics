using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    private Object armor;
    private Inventory inventory;

    public void OnEnable()
    {
        inventory = target as Inventory;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        armor = EditorGUILayout.ObjectField(armor, typeof(Armor), true);

        if(GUILayout.Button("Equip") && armor != null && inventory != null)
        {
            inventory.Equip(armor as Armor);
        }
    }
}
