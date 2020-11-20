using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultUI : MonoBehaviour
{
    public static DefaultUI instance = null;

    public Button endRoundButton;
    public Slider actionPointsSlider;

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
        actionPointsSlider.value = actionPoints;
    }

    public void SetUIInteractable(bool interactable)
    {
        endRoundButton.interactable = interactable;
        actionPointsSlider.interactable = interactable;
    }
}
