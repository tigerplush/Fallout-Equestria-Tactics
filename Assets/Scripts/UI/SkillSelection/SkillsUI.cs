using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsUI : MonoBehaviour
{
    public GameObject skillManipulatorPrefab;

    public ScriptableCharacter character;

    // Start is called before the first frame update
    void Start()
    {
        if(skillManipulatorPrefab != null && character != null)
        {
            foreach(SkillElement element in character.skills.indexElements)
            {
                GameObject manipulatorObject = Instantiate(skillManipulatorPrefab, transform);
                SkillManipulatorUI manipulatorUI = manipulatorObject.GetComponent<SkillManipulatorUI>();
                manipulatorUI.Setup(character, element);
            }
        }
    }
}
