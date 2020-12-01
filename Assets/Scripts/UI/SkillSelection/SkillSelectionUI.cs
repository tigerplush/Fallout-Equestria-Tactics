using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillSelectionUI : MonoBehaviour
{
    public IntAttributeObject test;

    private void Start()
    {
        Debug.Log(test.Value);
    }

    public void Back()
    {
        SceneManager.LoadScene("CharacterCreationMenu");
    }

    public void Next()
    {
        //SceneManager.LoadScene("");
    }
}
