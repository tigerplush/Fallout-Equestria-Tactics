using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPointsUI : MonoBehaviour
{
    public ActionPointUI[] pointUIs;

    public void SetInteractable(bool interactable)
    {
        for (int i = 0; i < pointUIs.Length; i++)
        {
            pointUIs[i].SetInteractable(interactable);
        }
    }

    public void SetValue(int value)
    {
        for(int i = 0; i < pointUIs.Length; i++)
        {
            pointUIs[i].SetActive(i < value);
        }
    }
}
