using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false; // On met un bool qui s'active quand le  menu est en pause 

    [SerializeField] private MonoBehaviour playerControlerScript; // on prends le script qui d�place le joueur

    public GameObject pauseMenuUI; // On prends l'objet qui contient l'ui du menu pause

    public GameObject settingsWindow; // On prends l'objet qui contient l'ui du menu pause


    public void Start()
    {
        playerControlerScript.enabled = true; // On active le script de d�placement
        pauseMenuUI.SetActive(false); // On d�sactive le menu pause
        Time.timeScale = 1; // On met l'�chelle du temps a 1
        gameIsPaused = false; // On met gameIsPaused faux
    }


   public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Si la touche Echap est pr�ss�
        {
            if (gameIsPaused) // Si gameIsPaused 
            {
                Resume(); // On appelle la fonction Resume
            }
            else
            {
                Paused(); // Sinon on appelle la fonction Paused
            }

        }
    }

    public void Paused()
    {
        playerControlerScript.enabled = false; // On d�sactive le script de d�placement
        pauseMenuUI.SetActive(true); // On active le menu pause
        Time.timeScale = 0; // On met l'�chelle du temps a 0
        gameIsPaused = true;  // On met gameIsPaused true
    }

    public void Resume()
    {
        playerControlerScript.enabled = true;  // On active le script de d�placement
        pauseMenuUI.SetActive(false); // On d�sactive le menu pause
        Time.timeScale = 1; // On met l'�chelle du temps a 1
        gameIsPaused = false; // On met gameIsPaused faux
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu"); //On charge la scene Menu
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true); //On active la fenetre d'option

    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false); //On d�sactive la fenetre d'option

    }
}
