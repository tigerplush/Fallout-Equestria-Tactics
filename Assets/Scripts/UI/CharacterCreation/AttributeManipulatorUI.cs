using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AttributeManipulatorUI : ManipulatorUI<AttributeElement>
{
    public override void Setup(ScriptableCharacter character, AttributeElement element)
    {
        base.Setup(character, element);
        if(character.attributes.Has(element))
        {
            stat = character.attributes.Get(element);
            UpdateUI();
            stat.ValueChanged += UpdateUI;
        }
    }

    public override void UpdateUI()
    {
        value.text = stat.Value.ToString();
    }

    public override void Increase()
    {
        if(character != null && stat != null && character.AttributePoints > 0)
        {
            if(stat.Increase(1))
            {
                character.AttributePoints -= 1;
            }
        }
    }

    public override void Decrease()
    {
        if (stat != null && character != null)
        {
            if (stat.Decrease(1))
            {
                character.AttributePoints += 1;
            }
        }
    }
}
