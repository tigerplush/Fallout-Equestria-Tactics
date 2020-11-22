using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPointUI : MonoBehaviour
{
    public GameObject disabledEmptyBar;
    public GameObject disabledBar;
    public GameObject activeBar;

    private bool isActive = false;

    public void SetInteractable(bool interactable)
    {
        disabledEmptyBar.SetActive(!interactable);
        activeBar.SetActive(isActive && interactable);
    }

    public void SetActive(bool active)
    {
        isActive = active;
        activeBar.SetActive(isActive);
        disabledBar.SetActive(isActive);
    }
}
