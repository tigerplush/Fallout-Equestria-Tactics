using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float lifespan = 3f;
    public AnimationCurve alphaFalloff;

    private float timeAlive = 0f;

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;
        Color color = text.color;
        color.a = alphaFalloff.Evaluate(timeAlive / lifespan);
        text.color = color;
    }

    public void Set(Vector3 position)
    {
        Vector3 textPosition = Camera.main.WorldToScreenPoint(position);
        Debug.Log(textPosition);
        transform.position = textPosition;
        text.color = Color.white;
        text.text = "miss";
    }

    public void Set(Vector3 position, float damage)
    {
        Set(position);
        text.color = Color.red;
        text.text = damage.ToString("F0");
    }
}
