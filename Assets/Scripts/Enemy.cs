using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    public float moveSpeed = 3f;
    public float jumpForce = 7f;
    public float detectionRange = 10f;
    public float attackDistance = 2f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public ParticleSystem bloodVFX;
    public LayerMask groundLayer;
    public int health = 100;
    public int damage = 5;
    public GameObject attackPrefab;
    public float attackCooldown = 0.75f;
    public float attackLifetime = 0.5f;
    public float attackChanceIncreaseRate = 7.5f;
    public int minGoldDrop = 15;
    public int maxGoldDrop = 25;
    public Sprite enemyHit;
    public Sprite enemyDefault;
    int goldDrop;
    public EnemySpawnHandler spawnHandler;
    Rigidbody2D rb;
    bool isGrounded = false;
    bool canAttack = true;
    float attackChance = 0f;
    Vector3 lastPlayerPosition;
    public bool isDead = false;
    public int enemyAttackType;
    public GameObject enemySword;
    public float attackOffset;

    void Start()
    {
        health = 95 + FindObjectOfType<EnemyLevelManager>().enemyLevel * 25;
        damage = 15 + FindObjectOfType<EnemyLevelManager>().enemyLevel * 5;
        player = FindObjectOfType<PlayerController>().transform;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= attackDistance)
            {
                rb.velocity = Vector2.zero;

                if (player.position != lastPlayerPosition)
                {
                    lastPlayerPosition = player.position;
                }

                attackChance += attackChanceIncreaseRate * Time.deltaTime;
                attackChance = Mathf.Clamp(attackChance, 0f, 100f);

                if (canAttack && Random.Range(0f, 100f) < attackChance)
                {
                    PerformAttack();
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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float direction = Mathf.Sign(player.position.x - transform.position.x);
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        if (direction > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    void PerformAttack()
    {
        canAttack = false;
        rb.velocity = Vector2.zero;
        StandardAttack();
        attackChance = 0f;
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void StandardAttack()
    {
        Vector3 direction = GetAttackDirection();
        Vector3 spawnPosition = transform.position + direction;
        Instantiate(enemySword, spawnPosition, Quaternion.identity);
        GameObject attack = Instantiate(attackPrefab, spawnPosition, Quaternion.identity);
        Destroy(attack, attackLifetime);
    }

    Vector3 GetAttackDirection()
    {
        enemyAttackType = 0;
        Vector3 toPlayer = player.position - transform.position;
        if (Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y))
        {
            if(toPlayer.x > 0)
            {
                enemyAttackType = 2;
                return Vector3.right * attackOffset;
            }
            else
            {
                enemyAttackType = 4;
                return Vector3.left * attackOffset;
            }
        }
        else
        {
            if (toPlayer.y > 0)
            {
                enemyAttackType = 1;
                return Vector3.up * attackOffset;
            }
            else
            {
                enemyAttackType = 3;
                return Vector3.down * attackOffset;
            }
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

    public void HitAnimation()
    {
        StartCoroutine(HitSprites());
    }
    public IEnumerator HitSprites()
    {
        Instantiate(bloodVFX, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().sprite = enemyHit;
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().sprite = enemyDefault;
    }


    void Die()
    {
        isDead = true;

        if (spawnHandler != null)
        {
            spawnHandler.hasSpawnedEnemy = false;
        }

        goldDrop = Random.Range(minGoldDrop, maxGoldDrop) + FindObjectOfType<EnemyLevelManager>().enemyLevel * 5;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        FindObjectOfType<EnemyLevelManager>().enemyLevel++;
        FindObjectOfType<GoldManager>().AddGold(goldDrop);
        FindObjectOfType<ExpeditionManager>().AddRoundPassed();
        FindObjectOfType<RoundManager>().roundsPassed++;
        Destroy(gameObject, 0.001f);
    }

    public int GetGoldDropped()
    {
        return goldDrop;
    }

    public void SetSpawnHandler(EnemySpawnHandler handler)
    {
        spawnHandler = handler;
    }
    public void PlayerIsDying()
    {
        GetComponent<Enemy>().enabled = false;
    }
}
