using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttributeObject<T> : ScriptableObject
{
    public T minValue;
    public T maxValue;

    [SerializeField]
    protected T m_Value;

    public abstract T Value { get; set; }

    public delegate void ValueChangedHandler();
    public ValueChangedHandler ValueChanged;

    public abstract bool Increase(T increment);
    public abstract bool Decrease(T decrement);
}
