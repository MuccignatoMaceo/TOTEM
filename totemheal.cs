using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totemheal : MonoBehaviour
{
    [SerializeField] public LifeAndDeath lifeAndDeath;
    public HealthController healthController;
    
    public int healthBonus = 3; //On crée une variable pour la vie bonus


    void Start()
    {
        lifeAndDeath = FindObjectOfType<LifeAndDeath>(); // On cherche le script LifeAndDeath;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lifeAndDeath.hp < lifeAndDeath.hpMax) //Si les hp sont inférieurs a hpMax
        {
            Destroy(gameObject); //On détruit l'objet qui a le script
            lifeAndDeath.hp = lifeAndDeath.hp + healthBonus; //On augmente la valeur de hp en fonction du bonus de hp

            if (lifeAndDeath.hp > lifeAndDeath.hpMax) //Si les hp depassent hpMax
            {
                lifeAndDeath.hp = lifeAndDeath.hpMax; //Hp est égal a hpMax
                healthController.SetMaxHealth(healthBonus); // On met a jour la valeur du slider de vie en fonctioàn de la vie

            }
        }

    }

}