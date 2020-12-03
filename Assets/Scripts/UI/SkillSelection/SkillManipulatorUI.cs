using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillManipulatorUI : AttributeManipulatorUI
{
    public TextMeshProUGUI rawValue;

    public override void UpdateUI()
    {
        value.text = stat.Value.ToString();
        rawValue.text = stat.RawValue.ToString();
    }

    public override void Increase()
    {
        if (stat != null && character != null && character.SkillPoints > 0)
        {
            if (stat.Increase(1))
            {
                character.SkillPoints -= 1;
            }
        }
    }

    public override void Decrease()
    {
        if (stat != null && character != null)
        {
            if (stat.Decrease(1))
            {
                character.SkillPoints += 1;
            }
        }
    }
}
