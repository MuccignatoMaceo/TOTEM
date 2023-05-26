using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private int damages = 50; //On crée une variable pour les dégats de la zone
    public LifeAndDeath lifeAndDeath; //On prend le script LifeAndDeath


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //Si un object qui rentre en collision a le tag Player
        {
            lifeAndDeath.takeDamage(damages); // On appelle la fonction takeDamage de LifeAndDeath avec la valeur de Damage
        }
    }
}
