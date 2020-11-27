using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitChanceCanvasUI : MonoBehaviour
{
    public TextMeshProUGUI hitChanceText;

    private Quaternion rotation;

    public void Awake()
    {
        rotation = transform.rotation;
    }

    public void Enable(float chance)
    {
        transform.rotation = rotation;
        hitChanceText.text = $"{chance:F2}%";
        hitChanceText.gameObject.SetActive(true);
    }

    public void Disable()
    {
        hitChanceText.gameObject.SetActive(false);
    }
}
