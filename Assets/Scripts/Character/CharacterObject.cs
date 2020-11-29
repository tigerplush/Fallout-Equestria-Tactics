using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Character Object")]
public class CharacterObject : ScriptableObject
{
    public RaceElement Race;

    [SerializeField]
    private int m_AttributePoints;
    public int attributePoints
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

    public IntAttributeObject Strength;
    public IntAttributeObject Perception;
    public IntAttributeObject Endurance;
    public IntAttributeObject Charisma;
    public IntAttributeObject Intelligence;
    public IntAttributeObject Agility;
    public IntAttributeObject Luck;

    public delegate void StatsChangedHandler();
    public StatsChangedHandler StatsChanged;
}
