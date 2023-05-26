using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; //On prend le mixer d'audio


    public void SetVolume(float volume) 
    {
        audioMixer.SetFloat("volume", volume);  //On change la valeur du mixer
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen; // On met la fenetre en plein écran
    }

}
