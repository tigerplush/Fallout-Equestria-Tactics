using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillSelectionUI : MonoBehaviour
{
    private void Start()
    {
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
