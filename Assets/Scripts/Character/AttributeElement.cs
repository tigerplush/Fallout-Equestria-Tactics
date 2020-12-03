using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Attribute Element", order = 8)]
public class AttributeElement : Element, IComparable<AttributeElement>
{
    public string description;
    public int priority;

    public int CompareTo(AttributeElement other)
    {
        return priority.CompareTo(other.priority);
    }
}
