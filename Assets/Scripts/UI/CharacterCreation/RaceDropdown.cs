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

    public CharacterObject character;

    private void Start()
    {
        if (dropdown != null)
        {
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            foreach (RaceElement race in selectableRaces)
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(race.name);
                options.Add(optionData);
            }
            dropdown.AddOptions(options);
            dropdown.RefreshShownValue();
            if (selectableRaces.Length > 0)
            {
                dropdown.onValueChanged.Invoke(0);
            }
        }
    }

    public void OptionChanged(int newValue)
    {
        character.Race = selectableRaces[newValue];
    }

    public void Enter(int childElement)
    {
        tooltip.Value = selectableRaces[childElement - 1].description;
    }

    public void Exit()
    {
        tooltip.Enabled = false;
    }
}
