using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private LifeAndDeath lifeanddeath; //On prend le script LifeAndDeath
    [SerializeField] public totemheal Totemheal; //On prends le script TotemHeal

    public Slider slider; //On prends l'object Slider

    public Gradient gradient; //On indique le gradient du slider
    public Image fill; //On prends la barre de remplissage du Slider
    
    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = lifeanddeath.hpMax; // On met la valeur maximale du slider en fonction de la vie maximale du joueur 
        slider.value = lifeanddeath.hp; // On met la valeur du slider en fonction de la vie du joueur 


        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = lifeanddeath.hp; //On met la valeur du slider en fonction de la vie du joueur


        fill.color = gradient.Evaluate(slider.normalizedValue);
    }


}
