using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UI/Text Attribute")]
public class TextAttribute : AttributeObject<string>
{
    [SerializeField]
    private bool m_Enabled = false;

    public bool Enabled
    {
        get
        {
            return m_Enabled;
        }
        set
        {
            m_Enabled = value;
            ValueChanged?.Invoke();
        }
    }

    public override string Value
    {
        get
        {
            return m_Value;
        }
        set
        {
            m_Enabled = true;
            m_Value = value;
            ValueChanged?.Invoke();
        }
    }

    public void Set(string value)
    {
        Value = value;
    }

    public override bool Decrease(string decrement)
    {
        return false;
    }

    public override bool Increase(string increment)
    {
        return false;
    }
}
