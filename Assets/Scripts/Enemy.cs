using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    public float moveSpeed = 3f;
    public float jumpForce = 7f;
    public float detectionRange = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public int health = 100;
    public int damage = 5;
    public float attackDistance = 2f;
    public GameObject attackPrefab;
    public float attackCooldown = 1f;
    public float attackLifetime = 0.5f;
    public float attackChanceIncreaseRate = 7.5f;
    public int minGoldDrop = 15;
    public int maxGoldDrop = 25;
    public EnemySpawnHandler spawnHandler;

    Rigidbody2D rb;
    bool isGrounded;
    bool canAttack = true;
    float attackChance = 0f;
    Vector3 lastPlayerPosition;
    public bool isDead = false;
    int goldDropped;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= attackDistance)
            {
                rb.velocity = Vector2.zero;

                if (player.transform.position != lastPlayerPosition)
                {
                    lastPlayerPosition = player.transform.position;
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
        if (!canAttack) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        if (direction > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (player.transform.position.y > transform.position.y + 1f && isGrounded)
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
        Vector3 toPlayer = player.transform.position - transform.position;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    void ProcessHit(DamageDealer damageDealer)
    {
        if (isDead) return;

        health -= damageDealer.GetDamage();

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        int goldDrop = Random.Range(minGoldDrop, maxGoldDrop);
        Debug.Log($"Gold Dropped: {goldDrop}");
        goldDropped = goldDrop;
        GameManager.Instance.AddGold();
        GameManager.Instance.TriggerLevelUpScreen();
        Destroy(gameObject, 0.001f);
    }
    public int GetGoldDrop() { return goldDropped; }

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

    public void SetSpawnHandler(EnemySpawnHandler handler)
    {
        spawnHandler = handler;
    }
}

