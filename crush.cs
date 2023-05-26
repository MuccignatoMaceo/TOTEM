using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class crush : MonoBehaviour
{
    [SerializeField] private LayerMask PlayerLayers; // On prend la couche Player
    [SerializeField] private Transform crushCheck; // On indique la position du crucshCheck
    [SerializeField] private float crushCheckWidth; // On crée une variable pour gerer la largeur de crush check 
    [SerializeField] private float crushCheckHeight; // On crée une variable pour gerer la hauteur de crush check 
    public LifeAndDeath lifeAndDeath; //On prend le script LifeAndDeath
    public int damages = 100; // On crée une variable pour les dégats
  

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapBox(crushCheck.position, new Vector2(crushCheckWidth, crushCheckHeight), 0f, PlayerLayers) != null) // Si une zone qui est a la position de crushCheck et qui a les dimensions de crushCheckWidth et crushCheckHeight et qui touche la couche Player
        {
            lifeAndDeath.takeDamage(damages); // On lance la fonction takeDamage avec la variable Damages
        }
    }
}
