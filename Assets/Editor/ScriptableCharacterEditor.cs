using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScriptableCharacter))]
public class ScriptableCharacterEditor : Editor
{
    private ScriptableCharacter character;

    private void OnEnable()
    {
        character = serializedObject.targetObject as ScriptableCharacter;
    }

    public override void OnInspectorGUI()
    {
        if(character.attributes != null && character.skills != null)
        {
            base.OnInspectorGUI();
        }
        else
        {
            if (GUILayout.Button("Initialise"))
            {
                ScriptableCharacter character = serializedObject.targetObject as ScriptableCharacter;
                character.CreateSubAssets();
                AssetDatabase.AddObjectToAsset(character.attributes, character);
                AssetDatabase.AddObjectToAsset(character.skills, character);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
