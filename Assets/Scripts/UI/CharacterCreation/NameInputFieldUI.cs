using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameInputFieldUI : MonoBehaviour
{
    public TMP_InputField inputField;
    public CharacterObject character;

    public void OnEnable()
    {
        inputField.text = character.CharacterName;
    }

    public void OnValueChanged(string value)
    {
        character.CharacterName = value;
    }
}
