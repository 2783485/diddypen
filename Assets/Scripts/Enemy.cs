using System.Collections;
using Unity.Services.CloudSave.Models.Data.Player;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    PlayerController player;
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
    public GameObject purpleOrbPrefab;
    public float attackCooldown = 0.75f;
    public float purpleOrbCooldown = 3f;
    public float attackLifetime = 0.5f;
    public float attackChanceIncreaseRate = 7.5f;
    public int minGoldDrop = 15;
    public int maxGoldDrop = 25;
    int goldDrop;
    public EnemySpawnHandler spawnHandler;
    Rigidbody2D rb;
    bool isGrounded = false;
    bool canAttack = true;
    bool canShootOrb = true;
    float attackChance = 0f;
    Vector3 lastPlayerPosition;
    public bool isDead = false;
    public int enemyAttackType;
    public GameObject enemySword;
    public float attackOffset;
    public bool hitStopOn;
    public Sprite lowLevelSprite;
    public Sprite lowLevelSpriteHit;
    public Sprite paladinSprite;
    public Sprite paladinSpriteHit;
    public Sprite eldritchKnightSprite;
    public Sprite eldritchKnightSpriteHit;
    public int enemyType;
    public bool hitBySword;

    void Start()
    {
        if (FindObjectOfType<EnemyLevelManager>().enemyLevel >= 9 && FindObjectOfType<EnemyLevelManager>().enemyLevel < 24)
        {
            enemyType = Random.Range(0, 3);
            if (enemyType == 0)
            {
                GetComponent<SpriteRenderer>().sprite = lowLevelSprite;
            }
            else if (enemyType == 1)
            {
                GetComponent<SpriteRenderer>().sprite = paladinSprite;
            }
            else if (enemyType == 2)
            {
                GetComponent<SpriteRenderer>().sprite = eldritchKnightSprite;
            }
        }
        else if (FindObjectOfType<EnemyLevelManager>().enemyLevel >= 24)
        {
            enemyType = Random.Range(1, 3);
            if (enemyType == 1)
            {
                GetComponent<SpriteRenderer>().sprite = paladinSprite;
            }
            else if (enemyType == 2)
            {
                GetComponent<SpriteRenderer>().sprite = eldritchKnightSprite;
            }
        }

        if (FindObjectOfType<EnemyLevelManager>().enemyLevel <= 9)
        {
            health = 95 + FindObjectOfType<EnemyLevelManager>().enemyLevel * 10;
        }
        else if (FindObjectOfType<EnemyLevelManager>().enemyLevel > 9)
        {
            health = 95 + FindObjectOfType<EnemyLevelManager>().enemyLevel * 25;
            damage = 15 + FindObjectOfType<EnemyLevelManager>().enemyLevel * 5;
        }

        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= attackDistance)
            {
                rb.velocity = Vector2.zero;

                if (player.transform.position != lastPlayerPosition)
                {
                    lastPlayerPosition = player.transform.position;
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

                if (enemyType == 2 && canShootOrb && Random.Range(0f, 100f) < 10f)
                {
                    StartCoroutine(ShootPurpleOrb());
                }
            }
        }
    }

    void FollowPlayer()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
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
        StartCoroutine(StandardAttack());
        attackChance = 0f;
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    IEnumerator StandardAttack()
    {
        Vector3 direction = GetAttackDirection();
        Vector3 spawnPosition = transform.position + direction;
        Instantiate(enemySword, spawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        GameObject attack = Instantiate(attackPrefab, spawnPosition, Quaternion.identity);
        Destroy(attack, attackLifetime);
    }

    Vector3 GetAttackDirection()
    {
        enemyAttackType = 0;
        Vector3 toPlayer = player.transform.position - transform.position;
        if (Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y))
        {
            if (toPlayer.x > 0)
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

    IEnumerator ShootPurpleOrb()
    {
        canShootOrb = false;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject orb = Instantiate(purpleOrbPrefab, transform.position, Quaternion.identity);
        Rigidbody2D orbRb = orb.GetComponent<Rigidbody2D>();
        orbRb.velocity = direction * 8f;
        Destroy(orb, 3f);
        yield return new WaitForSeconds(purpleOrbCooldown);
        canShootOrb = true;
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
        if (isDead)
        {
            return;
        }

        int damageTaken = damageDealer.GetDamage();
        if (enemyType == 1)
        {
            damageTaken = damageTaken / 2;
        }

        health -= damageTaken;
        StartCoroutine(HitStop());

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
        if (enemyType == 0)
        {
            GetComponent<SpriteRenderer>().sprite = lowLevelSpriteHit;
        }
        else if (enemyType == 1)
        {
            GetComponent<SpriteRenderer>().sprite = paladinSpriteHit;
        }
        else if (enemyType == 2)
        {
            GetComponent<SpriteRenderer>().sprite = eldritchKnightSpriteHit;
        }

        yield return new WaitForSeconds(0.3f);

        if (enemyType == 0)
        {
            GetComponent<SpriteRenderer>().sprite = lowLevelSprite;
        }
        else if (enemyType == 1)
        {
            GetComponent<SpriteRenderer>().sprite = paladinSprite;
        }
        else if (enemyType == 2)
        {
            GetComponent<SpriteRenderer>().sprite = eldritchKnightSprite;
        }
    }

    void Die()
    {
        isDead = true;

        if (spawnHandler != null)
        {
            spawnHandler.hasSpawnedEnemy = false;
        }

        goldDrop = Random.Range(minGoldDrop, maxGoldDrop) + FindObjectOfType<EnemyLevelManager>().enemyLevel * 10;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        FindObjectOfType<Background>().RerollTime();
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

    IEnumerator HitStop()
    {
        hitStopOn = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.05f);
        Time.timeScale = 1f;
        hitStopOn = false;
    }
    public void StartFireSword()
    {
        StartCoroutine(SwordOnFire());
    }
    public IEnumerator SwordOnFire()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);
            health -= player.rubiesInBlade;
            HitAnimation();
            if (health <= 0)
            {
                Die();
            }
        }
    }
    public void FireProj()
    {
        StartCoroutine(ProjOnFire());
    }
    public IEnumerator ProjOnFire()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);
            health -= player.rubiesInProj;
            HitAnimation();
            if (health <= 0)
            {
                Die();
            }
        }
    }
    public void ColdSword()
    {
        StartCoroutine(SwordCold());
    }
    public IEnumerator SwordCold()
    {
        moveSpeed = 3;
        attackCooldown = 2;
        yield return new WaitForSeconds(0.5f * player.saphInBlade);
        moveSpeed = 5;
        attackCooldown = 1.2f;
    }
    public void StartProjCold()
    {
        StartCoroutine(ProjCold());
    }
    public IEnumerator ProjCold()
    {
        moveSpeed = 3;
        attackCooldown = 2;
        yield return new WaitForSeconds(0.5f * player.saphInProj);
        moveSpeed = 5;
        attackCooldown = 1.2f;
    }
    public void LightningSword()
    {
        StartCoroutine(SwordLightning());
    }
    public IEnumerator SwordLightning()
    {
        canAttack = false;
        moveSpeed = 0;
        yield return new WaitForSeconds(0.1f * player.topazInBlade);
        canAttack = true;
        moveSpeed = 5;
    }
    public void LightningProj()
    {
        StartCoroutine(ProjLightning());
    }
    public IEnumerator ProjLightning()
    {
        canAttack = false;
        moveSpeed = 0;
        yield return new WaitForSeconds(0.1f * player.topazInProj);
        canAttack = true;
        moveSpeed = 5;
    }
    public void CursedSword()
    {
        StartCoroutine(SwordCursed());
    }
    public IEnumerator SwordCursed()
    {
        damage -= damage / 2;
        yield return new WaitForSeconds(0.5f * player.bdInBlade);
    }
    public void CursedProj()
    {
        StartCoroutine(ProjCursed());
    }
    public IEnumerator ProjCursed()
    {
        damage -= damage / 2;
        yield return new WaitForSeconds(0.5f * player.bdInProj);
    }
}
