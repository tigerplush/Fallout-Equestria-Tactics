using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager instance = null;

    public GameObject damageTextPrefab;

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

    public void Damage(Vector3 position)
    {
        DamageText damageText = Instantiate(damageTextPrefab, transform).GetComponent<DamageText>();
        damageText.Set(position);
    }

    public void Damage(Vector3 position, float damage)
    {
        DamageText damageText = Instantiate(damageTextPrefab, transform).GetComponent<DamageText>();
        damageText.Set(position, damage);
    }
}
