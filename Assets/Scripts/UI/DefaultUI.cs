using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultUI : MonoBehaviour
{
    public static DefaultUI instance = null;

    public Button endRoundButton;
    public ActionPointsUI actionPointBar;

    public WeaponSlotUI primary;
    public WeaponSlotUI secondary;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EndRound()
    {
        BattleManager.instance.EndRound();
    }

    public void SetActionPoints(int actionPoints)
    {
        actionPointBar.SetValue(actionPoints);
    }

    public void SetUIInteractable(bool interactable)
    {
        endRoundButton.interactable = interactable;
        actionPointBar.SetInteractable(interactable);
        primary.button.interactable = interactable;
        secondary.button.interactable = interactable;
    }

    public void SetWeaponsDisplay(WeaponType type)
    {
        switch(type)
        {
            case WeaponType.Primary:
                primary.gameObject.SetActive(true);
                secondary.gameObject.SetActive(false);
                break;
            case WeaponType.Secondary:
                primary.gameObject.SetActive(false);
                secondary.gameObject.SetActive(true);
                break;
        }
    }

    public void UpdateWeaponsDisplay(Weapon primary, Weapon secondary)
    {
        this.primary.Set(primary);
        this.secondary.Set(secondary);
    }
}
