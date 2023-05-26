using UnityEngine;

public class PlateformeTimerPlus : MonoBehaviour
{
    [SerializeField] public float timeToLiveMax = 3f;
    [SerializeField] public float timeToLive;
    private Rigidbody2D rb;
    private BoxCollider2D myCollider;
    private int nbObjects;
    [SerializeField] private Transform[] listeDestinations;
    private Animator animator;
    private bool isFalling;
    [SerializeField] private LifeAndDeath lifeAndDeath;

    void Start()
    {
        timeToLive = timeToLiveMax;
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        Spawn();
    }

    private void Spawn()
    {
        transform.position = listeDestinations[0].position;
        myCollider.enabled = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        timeToLive = timeToLiveMax;
        animator.SetBool("isFalling", true);

    }

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        myCollider = GetComponent<BoxCollider2D>();
        nbObjects = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody2D rb))
        {
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                // On ajoute 1  au nombre d'objet en collision
                nbObjects += 1;
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody2D rb))
        {
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                // On enleve 1  au nombre d'objet en collision
                nbObjects -= 1;
            }
        }
    }

    void FixedUpdate()
    {
        if (nbObjects>0)
        {
            timeToLive -= Time.fixedDeltaTime;
            if (timeToLive <= 0)
            {
                rb.constraints = RigidbodyConstraints2D.None;
              
                myCollider.enabled = false;
                animator.SetBool("isFalling", false);
                Invoke("Spawn", 5f);
            }
        }

        if (lifeAndDeath.hp <= 0) // Si le joueur meurt 
        {
            timeToLive = timeToLiveMax; // Le temps de vie de la plateforme retourne au max
        }
    }
}
