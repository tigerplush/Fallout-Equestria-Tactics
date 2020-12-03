using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaceDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public RaceElement[] selectableRaces;
    public TextAttribute tooltip;

    public ScriptableCharacter character;

    private void Start()
    {
        if (dropdown != null)
        {
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            int selection = -1;

            for(int i = 0; i < selectableRaces.Length; i++)
            {
                RaceElement race = selectableRaces[i];
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(race.name);
                options.Add(optionData);

                if (character != null && character.Race != null && character.Race == race)
                {
                    selection = i;
                }
            }

            dropdown.AddOptions(options);
            dropdown.value = selection;
        }
    }

    public void OptionChanged(int newValue)
    {
        character.Race = selectableRaces[newValue];
    }

    public void Enter(int childElement)
    {
        tooltip.Set(selectableRaces[childElement - 1].description);
    }

    public void Exit()
    {
        tooltip.Enabled = false;
    }
}
