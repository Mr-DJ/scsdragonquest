using UnityEngine;

public class DragonEnemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f; // Speed at which the dragon moves up and down
    public float maxHeight = 5f; // Maximum height the dragon can reach
    public Transform groundCheck; // Object to check for ground detection
    public float groundCheckRadius = 0.1f; // Radius for ground check
    public LayerMask groundLayer; // Layer for detecting ground

    [Header("Attack Settings")]
    public GameObject fireballPrefab; // Fireball projectile prefab
    public Transform firePoint; // The point from where the fireball is fired
    public float attackRange = 10f; // Range within which the dragon can attack
    public float fireballSpeed = 5f; // Speed of the fireball
    public float timeBetweenAttacks = 2f; // Time between consecutive attacks

    private bool movingUp = true; // Determines if the dragon is moving up or down
    private Rigidbody2D rb;
    private Transform player;
    private float attackCooldownTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Configure Rigidbody2D properties for smooth floating movement
        rb.bodyType = RigidbodyType2D.Kinematic; 
        rb.gravityScale = 0;    
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        player = GameObject.FindGameObjectWithTag("Player").transform; // Find player by tag
    }

    void Update()
    {
        MoveDragon();
        HandleAttacks();
    }

    void MoveDragon()
    {
        if (movingUp)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, moveSpeed);

            if (transform.position.y >= maxHeight)
            {
                movingUp = false;
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, -moveSpeed);

            if (IsTouchingGround())
            {
                movingUp = true;
            }
        }
    }

    bool IsTouchingGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        return colliders.Length > 0;
    }

    void HandleAttacks()
    {
        attackCooldownTimer -= Time.deltaTime;

        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (attackCooldownTimer <= 0f)
            {
                FireFireball(); 
                attackCooldownTimer = timeBetweenAttacks; // Reset attack cooldown
            }
        }
    }

    void FireFireball()
    {
        if (fireballPrefab != null && firePoint != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rbFireball = fireball.GetComponent<Rigidbody2D>();

            if (rbFireball != null)
            {
                Vector2 direction = (player.position - firePoint.position).normalized;
                rbFireball.linearVelocity = direction * fireballSpeed;
            }
        }
        else
        {
            Debug.LogError("Fireball prefab or FirePoint not assigned.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(new Vector3(transform.position.x, maxHeight, transform.position.z), 0.5f);
        
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); 

        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, attackRange); // Visualize attack range in editor

        if (firePoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(firePoint.position, 1); 
        }
    }
}
