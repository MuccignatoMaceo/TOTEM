using UnityEngine;

public class PlayerMoveAndJump : MonoBehaviour
{
    private float horizontalMove;
    private bool isJumpingRequired;
    private bool isFalling;
    private bool isGrounded;
    private bool isCeiled;
    private bool isWalledLeft;
    private bool isWalledRight;
    
    private Vector2 zeroVelocity = Vector2.zero;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float initialGravityScale;
    private Animator animator;
    private bool isWalking;
    private bool isJumping;
    private bool isWalled;


    [SerializeField] private float speed = 6f;
    [SerializeField] private float ceilSpeedMultiplier = 0.2f;
    [SerializeField] private float movementSmoothing = 0.2f;
    [SerializeField] private float jumpForce = 6.5f;
    [SerializeField] private float ceilJumpMultiplier = 0.3f;
    [SerializeField] private float wallJumpMultiplier = 1f;
    [SerializeField] private float maxFallingMagnitudeOnWall = 5f;
    [SerializeField] private float velocityThreshold = 0.15f;
	[SerializeField] private float fallGravityMultiplier = 2.2f;
	[SerializeField] private float lowJumpGravityMultiplier = 2.5f;
    [SerializeField] private float wallGravityResist = 25f;

    // Entre 0 et 1f --> Lerp entre up et right
    [SerializeField] private float wallJumpAngle = 1f;
    
    [SerializeField] private LayerMask groundLayers;

    [SerializeField] private Transform groundCheck;	
    [SerializeField] private float groundCheckWidth;
    [SerializeField] private float groundCheckHeight;

    [SerializeField] private Transform ceilCheck;
    [SerializeField] private float ceilCheckWidth;
    [SerializeField] private float ceilCheckHeight;

    [SerializeField] private Transform wallCheckLeft;
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private float wallCheckWidth;
    [SerializeField] private float wallCheckHeight;

    [SerializeField] private float maxTimeOnCeiling = 2.5f;
    [SerializeField] private float timeOnCeiling = 0f;

    private void Awake()
	{
        // On récupère le composant Rigidbody2D du player
		rb = GetComponent<Rigidbody2D>();

        // On mémorise l'echelle de gravité de départ
        initialGravityScale = rb.gravityScale;

        // On récupère l'animateur du player
        animator = GetComponent<Animator>();

        // On récupère le composant SpriteRenderer du player
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

    void Update()
    {
        // On récupère une valeur qui vaudra -1 si on le joueur utilise la fleche gauche, 1 si il utilise la droite, et sinon 0
        horizontalMove = Input.GetAxisRaw("Horizontal");

        // Si on va à droite 
        if (horizontalMove > 0)
        {   
            // On ne fait pas de symétrie sur le sprite du player
            spriteRenderer.flipX = false;
        } // Sinon ET Si on va à gauche
		else if (horizontalMove < 0)
        {
          
            // On fait une symétrie sur le sprite du player
            spriteRenderer.flipX = true;
        }

        // Si il y a un mouvement horizontal
        if(horizontalMove!=0){
            // On dit à l'animateur qu'on est en mode course
            animator.SetBool("isWalking", true);
        }
        else
        {// Sinon
            // On dit à l'animateur qu'on est PAS en mode course
            animator.SetBool("isWalking", false);
        }

        // Sion utilise la touche de saut ET que on (touche le sol OU/ET qu'on touche le plafond)
        if (Input.GetButtonDown("Jump") && (isGrounded || isCeiled || isWalledLeft || isWalledRight))
        {
            // On dit qu'on veut sauter
            isJumpingRequired = true;

            animator.SetBool("isJumping", true);
        }

        animator.SetBool("isCeilled", isCeiled);
        animator.SetBool("isWalled", isWalled);
    }

    void FixedUpdate() {
        OnWall();

        // Une variable locale pour stocker la vitesse à appliquer, elle pourra être modifiée avant utilisation !
        float tempSpeed = speed;

        // On test si une zone rectangulaire au niveau des pieds ("groundCheck") se superpose à un ou plusieurs éléments du calque spécifié ("Ground")
        if (Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckWidth,groundCheckHeight),0f, groundLayers)!=null)
        {
            // Si on ne touchait pas le sol avant
            if(isGrounded == false){
                // On vient de faire un Aterissage !

                animator.SetBool("isJumping", false);   
               // animator.SetBool("isFalling", false);
                isFalling = false;

                // On passe la variable qui dit si on est au sol à VRAI
                isGrounded = true;
            }
        }
        else // SINON
        {
            // On touche pas le sol donc on passe la variable qui dit si on est au sol à FAUX
            isGrounded = false;
        }

        // On test si une zone rectangulaire au niveau de la tête ("ceilCheck") se superpose à un ou plusieurs éléments du calque spécifié ("Ground")
        if (Physics2D.OverlapBox(ceilCheck.position, new Vector2(ceilCheckWidth, ceilCheckHeight), 0f, groundLayers) != null)
        {
            // on touchait déja le plafond avec la tete
            if (isCeiled)
            {
                // On incrémente le compteur de temps passé au plafond avec le temps écoulé depuis le dernier appel à FixedUpdate
                timeOnCeiling += Time.fixedDeltaTime;

                // Si le temps passé au plafond dépasse ou atteint le temps maximum autorisé
                if (timeOnCeiling >= maxTimeOnCeiling)
                {
                    // Le temps est écoulé ! On doit devisser du plafond !
                    isCeiled = false;
                }
            }
            else // Sinon
            {
                // Je viens juste de me coller au plafond
                isCeiled = true;

                isJumping = false;
                // On met le compteur de temps passé au plafond à zéro
                timeOnCeiling = 0f;
            }
        }
        else
        {
            // Je ne touche pas le plafond avec ma tete
            isCeiled = false;
        }


        if (isCeiled)
        {
            rb.gravityScale = 0;
            tempSpeed *= ceilSpeedMultiplier;
        }
        else
        {
            rb.gravityScale = initialGravityScale;

            if(rb.velocity.y < -velocityThreshold){
                //animator.SetBool("isFalling", true); 
                isFalling = true;
                rb.gravityScale = initialGravityScale * fallGravityMultiplier;
            }else{
               // animator.SetBool("isFalling", false); 
                isFalling = false;
            }
        
            if(rb.velocity.y > velocityThreshold){
                if(Input.GetButton("Jump")){
                    rb.gravityScale = initialGravityScale ;
                }else{
                    rb.gravityScale = initialGravityScale * lowJumpGravityMultiplier;
                } 
            }
        }

        if ( (isWalledLeft || isWalledRight) && isFalling)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxFallingMagnitudeOnWall);
            
        }

        Vector2 targetVelocity = new Vector2(horizontalMove * tempSpeed, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref zeroVelocity, movementSmoothing);

        if (isJumpingRequired)
        {
            isJumpingRequired = false;

            if (isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
            }

            if (isCeiled)
            {
                rb.AddForce(Vector2.down * jumpForce * ceilJumpMultiplier, ForceMode2D.Impulse);
            }

            if (isWalledLeft && !isGrounded)
            {
                rb.AddForce(Vector2.Lerp(Vector2.up,Vector2.right,wallJumpAngle) * jumpForce * wallJumpMultiplier, ForceMode2D.Impulse);
            }

            if (isWalledRight && !isGrounded)
            {
                rb.AddForce(Vector2.Lerp(Vector2.up, Vector2.left, wallJumpAngle) * jumpForce * wallJumpMultiplier, ForceMode2D.Impulse);
            }

        }
    }
    private void OnWall()
    {

        if (Physics2D.OverlapBox(wallCheckLeft.position, new Vector2(wallCheckWidth, wallCheckHeight), 0f, groundLayers) != null)
        {
            isWalledLeft = true;
            Debug.Log(isWalled);
            if (horizontalMove < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallGravityResist, float.MaxValue));



            }
        }
        else
        {
            isWalledLeft = false;
        }

        if (Physics2D.OverlapBox(wallCheckRight.position, new Vector2(wallCheckWidth, wallCheckHeight), 0f, groundLayers) != null)
        {
            isWalledRight = true;
            if (horizontalMove > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallGravityResist, float.MaxValue));
            }
        }
        else
        {
            isWalledRight = false;
        }
        if (isWalledRight || isWalledLeft)
        {
            isWalled = true;
        }
        else
            isWalled = false;
    }
    void OnDrawGizmos()
    {
        // Dessine un cube vert à la position du groundCheck
        Gizmos.color = new Color32(0, 255, 0, 90);
        Gizmos.DrawCube(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight));
        Gizmos.DrawCube(ceilCheck.position, new Vector2(ceilCheckWidth, ceilCheckHeight));
        Gizmos.DrawCube(wallCheckLeft.position, new Vector2(wallCheckWidth, wallCheckHeight));
        Gizmos.DrawCube(wallCheckRight.position, new Vector2(wallCheckWidth, wallCheckHeight));
    }
}
