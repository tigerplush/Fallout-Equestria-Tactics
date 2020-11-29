using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CharacterCreationUI : MonoBehaviour
{
    public TextMeshProUGUI attributePoints;

    public CharacterObject character;

    private void OnEnable()
    {
        if(character != null)
        {
            character.StatsChanged += UpdateUI;
            UpdateUI();
        }
    }

    private void OnDisable()
    {
        if(character)
        {
            character.StatsChanged -= UpdateUI;
        }
    }

    private void UpdateUI()
    {
        if(attributePoints != null)
        {
            attributePoints.text = character.attributePoints.ToString();
        }
    }
}
