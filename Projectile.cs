using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float speed = 5f; // On crée une variable pour la vitesse du projectile
    [SerializeField] private int damages = 1; // On crée une variable pour les dégats du projectile
    public LifeAndDeath lifeAndDeath; //On prend le script LifeAndDeath


   
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed); // On fait avancer le projectile en avant en fonction du temps et de la vitesse
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player") // Si il touche un objet avec le tag player
        {
            lifeAndDeath.takeDamage(damages); // On appelle la fonction takeDamage de LifeAndDeath avec la valeur de Damage
            Destroy(gameObject); // On détruit l'objet
        }
        else 
        {
            Destroy(gameObject); // On détruit l'objet
        }

    }

    public void setSpeed (float newSpeed)
    {
        speed = newSpeed;
    }

    public void setDamage (int newDamage)
    {
        damages = newDamage;
    }
}
