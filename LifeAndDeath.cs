using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class LifeAndDeath : MonoBehaviour
{
    
    [SerializeField] public int hpMax = 3; // On met la valeur de vie max a 3
    [SerializeField] public int hp; //On crée une variable pour la vie 
    [SerializeField] private float respawnTime = 1; //On crée une varoiable pour le temps de réaparition
    [SerializeField] public Vector3 spawnPointPosition; // On mets un Vector3 pour la zone de réaparition du joueur 
    [SerializeField] private MonoBehaviour playerControlerScript; // On indique le composant qui permet au joueur de se déplacer
    private Rigidbody2D rb; //On indique le composant Rigidbody2D
    public HealthController healthController; //On indique le script qui controlle le slider la vie
    public SpriteRenderer spriteRenderer;   // On prend le spriteRenderer
    Color originalColor;  // On prend la couleur originale du sprite
    public float Flashtime = 0.5f;  //On met la valeutr 0.5 a la variable FlashTime

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        Spawn();
    }

    public void takeDamage(int damages)
    {
        hp -= damages;
        healthController.SetMaxHealth(hp);
        print(damages);
        DamageFlashStart();


        if (hp <= 0)
        {
            
            playerControlerScript.enabled = false;
            Invoke("Spawn", respawnTime);
        }
    }


    public void updateSpawnPosition(Vector3 newPosition)
    {
        spawnPointPosition = newPosition;

    }


    private void Spawn()
    {
        playerControlerScript.enabled = true;
        hp = hpMax;
        rb.velocity = Vector2.zero;
        transform.position = spawnPointPosition;
        healthController.SetMaxHealth(hpMax);

    }

    void DamageFlashStart()
    {

        spriteRenderer.color = new Color(255f, 255f, 255f, 0.5f);
        Invoke("DamageFlashStop", Flashtime);

    }

    void DamageFlashStop()
    {
        spriteRenderer.color = originalColor;
    }
}
