using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; 
    Color originalColor; 


    public float Flashtime = 0.5f; //On met la valeutr 0.5 a la variable FlashTime

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // On prend le spriteRenderer
        originalColor = spriteRenderer.color; // On prend la couleur originale du sprite


    }


    private void OnTriggerEnter2D(Collider2D collision) // La fonction commence quand on interagit avec le collider de l'objet
    {
        if (collision.gameObject.CompareTag("Player")) // Si l'objet qui rentre en collision a le tag
        {
            collision.gameObject.GetComponent<LifeAndDeath>().updateSpawnPosition(transform.position); //On prends le composant LifeAndDeath de l'objet qui est rentré en collision et on appelle la fonction updateSpawnPosition

            spriteRenderer.color = new Color(0f, 255f, 0, 1f); // On change la couleur du sprite
            Invoke("DamageFlashStop", Flashtime); //On appelle la fonction DamageFlashStop au bout d'un certains temps, ici la valeur de flashTime
        }

    }

    void DamageFlashStop()
    {
        spriteRenderer.color = originalColor; //On remet la couleur originale du sprite


    }
}
