using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class SkillSelectionUI : MonoBehaviour
{
    public TextMeshProUGUI skillPoints;

    public ScriptableCharacter character;

    private void OnEnable()
    {
        if(character != null)
        {
            character.StatsChanged += UpdateUI;
        }
    }

    private void OnDisable()
    {
        if(character != null)
        {
            character.StatsChanged -= UpdateUI;
        }
    }

    private void Start()
    {
        if(character != null)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if(skillPoints != null)
        {
            skillPoints.text = character.SkillPoints.ToString();
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("CharacterCreationMenu");
    }

    public void Next()
    {
        //SceneManager.LoadScene("");
    }
}
