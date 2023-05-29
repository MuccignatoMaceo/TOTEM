using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class fondu : MonoBehaviour
{
    private float Transparence; // On crée un float Transparence pour gerer le fondu
    public bool FadeOut; // On crée un bool pour enclencher le fondu 
    public float Step;

    private void Start()
    {
        Transparence = 1; // On met la valeur de la transparence a 1
    }

    private void Update()
    {
        Transparence = Mathf.Clamp(Transparence, 0, 1); 
        if(FadeOut)
        {
            Transparence += Step;
        }
        else
        {
            Transparence -= Step;
        }
        GetComponent<CanvasGroup>().alpha = Transparence; // On change la valeur de l'alpha du canvas en fonction de la valeur de Transparence
    }
}
