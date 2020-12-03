using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public abstract class ManipulatorUI<T> : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler where T : Element
{
    public TextMeshProUGUI label;
    public TextMeshProUGUI value;
    public TextAttribute tooltip;

    public ScriptableCharacter character;
    public IntAttributeObject stat;
    public T element;

    protected void OnDisable()
    {
        if (stat != null)
        {
            stat.ValueChanged -= UpdateUI;
        }
    }

    public virtual void Setup(ScriptableCharacter character, T element)
    {
        this.character = character;
        this.element = element;
        label.text = element.name;
    }

    public abstract void UpdateUI();
    public abstract void Increase();
    public abstract void Decrease();

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltip != null && element != null)
        {
            tooltip.Set(element.description);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltip != null)
        {
            tooltip.Enabled = false;
        }
    }
}
