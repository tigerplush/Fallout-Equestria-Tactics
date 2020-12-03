using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Scriptable Character")]
public class ScriptableCharacter : ScriptableObject
{
    public AttributeIndex attributes;
    public SkillIndex skills;

    [SerializeField]
    private RaceElement m_Race;
    public RaceElement Race
    {
        get
        {
            return m_Race;
        }
        set
        {
            m_Race = value;
        }
    }

    [SerializeField]
    private string m_CharacterName;
    public string CharacterName
    {
        get
        {
            return m_CharacterName;
        }
        set
        {
            m_CharacterName = value;
            StatsChanged?.Invoke();
        }
    }

    [SerializeField]
    private int m_AttributePoints;
    public int AttributePoints
    {
        get
        {
            return m_AttributePoints;
        }
        set
        {
            m_AttributePoints = value;
            StatsChanged?.Invoke();
        }
    }

    [SerializeField]
    private int m_SkillPoints;
    public int SkillPoints
    {
        get
        {
            return m_SkillPoints;
        }
        set
        {
            m_AttributePoints = value;
            StatsChanged?.Invoke();
        }
    }

    public delegate void StatsChangedDelegate();
    public StatsChangedDelegate StatsChanged;

    public void CreateSubAssets()
    {
        attributes = CreateInstance<AttributeIndex>();
        skills = CreateInstance<SkillIndex>();
        attributes.name = "Attributes";
        attributes.parent = this;
        skills.name = "Skills";
        skills.parent = this;
    }

    public int NumberOfAttributes
    {
        get
        {
            return attributes.indexElements.Count;
        }
    }

    public AttributeElement AttributeElement(int index)
    {
        return attributes.indexElements[index];
    }

    public IntAttributeObject AttributeValue(int index)
    {
        return attributes.indexValues[index];
    }
}
