using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillManipulatorUI : ManipulatorUI<SkillElement>
{
    public TextMeshProUGUI rawValue;

    public override void Setup(ScriptableCharacter character, SkillElement element)
    {
        base.Setup(character, element);
        if (character.skills.Has(element))
        {
            stat = character.skills.Get(element);
            UpdateUI();
            stat.ValueChanged += UpdateUI;
        }
    }

    public override void UpdateUI()
    {
        float skillValue = character.skills.Value(element);
        int roundedValue = Mathf.FloorToInt(skillValue);
        value.text = $"({roundedValue.ToString()})";
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
