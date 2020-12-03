using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesUI : MonoBehaviour
{
    public GameObject attributeManipulatorPrefab;
    public ScriptableCharacter character;

    private void Start()
    {
        for(int i = 0; i < character.NumberOfAttributes; i++)
        {
            GameObject manipulatorObject = Instantiate(attributeManipulatorPrefab, transform);
            AttributeManipulatorUI manipulatorUI = manipulatorObject.GetComponent<AttributeManipulatorUI>();
            manipulatorUI.Setup(character, character.AttributeElement(i));
        }
    }
}
