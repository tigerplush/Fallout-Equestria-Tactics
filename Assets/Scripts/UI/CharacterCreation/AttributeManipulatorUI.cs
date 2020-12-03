using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AttributeManipulatorUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI label;
    public TextMeshProUGUI value;
    public TextAttribute tooltip;

    public ScriptableCharacter character;
    public AttributeElement attribute;
    public IntAttributeObject stat;

    private void OnDisable()
    {
        if (stat != null)
        {
            stat.ValueChanged -= UpdateUI;
        }
    }

    public void Setup(ScriptableCharacter character, AttributeElement attribute)
    {
        this.character = character;
        this.attribute = attribute;
        label.text = attribute.name;
        if(character.attributes.Has(attribute))
        {
            stat = character.attributes.Get(attribute);
            UpdateUI();
            stat.ValueChanged += UpdateUI;
        }
    }

    public virtual void UpdateUI()
    {
        value.text = character.attributes.Get(attribute).Value.ToString();
    }

    public virtual void Increase()
    {
        if(character != null && stat != null && character.AttributePoints > 0)
        {
            if(stat.Increase(1))
            {
                character.AttributePoints -= 1;
            }
        }
    }

    public virtual void Decrease()
    {
        if (stat != null && character != null)
        {
            if (stat.Decrease(1))
            {
                character.AttributePoints += 1;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(tooltip != null && attribute != null)
        {
            tooltip.Set(attribute.description);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(tooltip != null)
        {
            tooltip.Enabled = false;
        }
    }
}
