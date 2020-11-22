﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultUI : MonoBehaviour
{
    public static DefaultUI instance = null;

    public Button endRoundButton;
    public ActionPointsUI actionPointBar;

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
    }
}
