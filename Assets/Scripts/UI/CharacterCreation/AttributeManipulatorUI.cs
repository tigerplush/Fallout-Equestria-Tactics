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

    public CharacterObject character;
    public IntAttributeObject stat;
    public AttributeElement attribute;

    private void OnEnable()
    {
        if(stat != null)
        {
            stat.ValueChanged += UpdateUI;
        }
    }

    private void OnDisable()
    {
        if (stat != null)
        {
            stat.ValueChanged -= UpdateUI;
        }
    }

    private void Start()
    {
        if (stat != null)
        {
            UpdateUI();
        }
        if(attribute != null)
        {
            label.text = attribute.name;
        }
    }

    public void UpdateUI()
    {
        value.text = stat.Value.ToString();
    }

    public void Increase()
    {
        if(stat != null && character != null && character.attributePoints > 0)
        {
            if(stat.Increase(1))
            {
                character.attributePoints--;
            }
        }
    }

    public void Decrease()
    {
        if (stat != null)
        {
            if (stat.Decrease(1))
            {
                character.attributePoints++;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(tooltip != null && attribute != null)
        {
            tooltip.Value = attribute.description;
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
