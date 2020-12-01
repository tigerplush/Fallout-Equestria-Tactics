using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameInputFieldUI : MonoBehaviour
{
    public CharacterObject character;

    public void OnValueChanged(string value)
    {
        character.CharacterName = value;
    }
}
