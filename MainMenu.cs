using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                                                                                  

public class MainMenu : MonoBehaviour
{
    public string leveltoLoad;

    public GameObject settingsWindow;

    public void StartGame()
    {
        SceneManager.LoadScene(leveltoLoad); // On charge la scene qu'on a indiqué 

    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true); // On active la fenetre d'options

    }
    
    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false); // On active la fenetre d'options

    }

    public void QuitGame()
    {
        Application.Quit(); // On ferme le jeu

    }
}
