using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Scriptable Character")]
public class ScriptableCharacter : ScriptableObject
{
    public AttributeIndex attributes;
    public SkillIndex skills;

    private void OnEnable()
    {
    }

    public void CreateSubAssets()
    {
        attributes = CreateInstance<AttributeIndex>();
        skills = CreateInstance<SkillIndex>();
        attributes.name = "Attributes";
        attributes.parent = this;
        skills.name = "Skills";
        skills.parent = this;
    }
}
