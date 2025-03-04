using UnityEngine;

public class AIFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float jumpForce = 7f;
    public float detectionRange = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public int health;
    public int damage = 5;
    public float attackDistance = 2f;
    public GameObject attackPrefab;
    public float attackCooldown = 1f;
    public float attackLifetime = 0.5f;
    public float attackChanceIncreaseRate = 5f; // Reduced from 10f

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canAttack = true;
    private float attackChance = 0f;
    private Vector3 lastPlayerPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (player == null)
        {
            Debug.Log("No player");
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= attackDistance)
            {
                rb.velocity = Vector2.zero;

                if (player.position != lastPlayerPosition)
                {
                    lastPlayerPosition = player.position;
                    attackChance = 0f;
                }

                attackChance = Mathf.Clamp(attackChance + attackChanceIncreaseRate * Time.deltaTime, 0f, 100f);

                if (canAttack && Random.Range(0f, 100f) < attackChance)
                {
                    Attack();
                }
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    void FollowPlayer()
    {
        if (!canAttack)
            return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        if (direction > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (player.position.y > transform.position.y + 1f && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void Attack()
    {
        canAttack = false;
        rb.velocity = Vector2.zero;

        Vector3 attackDirection = GetAttackDirection();
        Vector3 spawnPosition = transform.position + attackDirection;

        GameObject attack = Instantiate(attackPrefab, spawnPosition, Quaternion.identity);
        Destroy(attack, attackLifetime);

        attackChance = 0f;
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    Vector3 GetAttackDirection()
    {
        Vector3 toPlayer = player.position - transform.position;
        if (Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y))
        {
            return toPlayer.x > 0 ? Vector3.right : Vector3.left;
        }
        else
        {
            return toPlayer.y > 0 ? Vector3.up : Vector3.down;
        }
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0) { Debug.Log("Enemy dead"); }
    }
}
