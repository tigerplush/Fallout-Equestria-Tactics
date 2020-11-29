using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public TextMeshProUGUI tooltip;
    public TextAttribute attributeToWatch;

    private void OnEnable()
    {
        if(attributeToWatch != null)
        {
            attributeToWatch.ValueChanged += UpdateUI;
        }
    }

    private void OnDisable()
    {
        if (attributeToWatch != null)
        {
            attributeToWatch.ValueChanged -= UpdateUI;
        }
    }

    private void UpdateUI()
    {
        if(tooltip != null && attributeToWatch != null)
        {
            tooltip.text = attributeToWatch.Value;
            tooltip.enabled = attributeToWatch.Enabled;
        }
    }
}
