using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class CharacterCreationUI : MonoBehaviour
{
    public TextMeshProUGUI attributePoints;
    public Button nextScreenButton;

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
            attributePoints.text = character.AttributePoints.ToString();
        }

        if(nextScreenButton != null)
        {
            bool characterName = character.CharacterName != "";
            bool attributePoints = character.AttributePoints == 0;
            nextScreenButton.interactable = characterName && attributePoints;
        }
    }

    public void Cancel()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Next()
    {
        SceneManager.LoadScene("SkillSelectionMenu");
    }
}
