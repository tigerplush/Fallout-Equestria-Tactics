using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("CharacterCreationMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
