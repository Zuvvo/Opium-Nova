using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject Menu;

    public bool IsMenuOpen = false;

    public void OpenCloseGameMenuButton()
    {
        if (!IsMenuOpen)
        {
            Menu.SetActive(true);
            IsMenuOpen = true;
            Time.timeScale = 0;
        }
        else
        {
            Menu.SetActive(false);
            IsMenuOpen = false;
            Time.timeScale = 1;
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}