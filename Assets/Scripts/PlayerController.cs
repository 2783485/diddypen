using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int playerLevel;
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float fastFallGravityScale = 4f;
    public float coyoteTime = 0.1f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask enemyAttackLayer;
    public LayerMask enemyLayer;
    public float groundCheckRadius = 0.2f;
    public GameObject attackPrefab;
    public GameObject arcaneProjectilePrefab;
    public float attackOffset = 1f;
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public bool isDashing = false;
    public bool hasIFrames = false;
    float dashCooldownTimer = 0f;
    public float attackCooldown = 1f;
    float attackCooldownTimer = 0f;
    public float attackDuration = 0.05f;
    public int health;
    public int maxHealth;
    public int strength;
    public int arcana;
    public int vitality;
    public float agility;
    bool healthFixed;
    public int levelUpCost;

    bool isAttacking = false;
    Rigidbody2D rb;
    bool isGrounded;
    float coyoteTimeCounter;
    float defaultGravityScale;
    bool isFacingRight = true;
    Collider2D coli;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coli = GetComponent<Collider2D>();
        defaultGravityScale = rb.gravityScale;
        rb.freezeRotation = true;
        maxHealth = 10 + vitality * 5;
        levelUpCost = 20 + playerLevel * 10;
        FixHealth();
    }

    void Update()
    {
        levelUpCost = 20 + playerLevel * 10;
        if (!isAttacking)
            Move();
        else if (isAttacking)
            rb.velocity = Vector2.zero;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            rb.gravityScale = defaultGravityScale;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            if (rb.velocity.y <= 0)
                rb.gravityScale = fastFallGravityScale;
        }

        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0)
            Jump();

        if (attackCooldownTimer > 0)
            attackCooldownTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && attackCooldownTimer <= 0)
        {
            Attack();
            attackCooldownTimer = 1f / attackCooldown;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
            Fire();

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0)
            StartCoroutine(Dash());

        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;
    }

    void Move()
    {
        if (isDashing) return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0 && !isFacingRight)
            Flip();
        else if (moveInput < 0 && isFacingRight)
            Flip();
    }

    void Fire()
    {
        Vector3 spawnPosition = transform.position;
        if (isFacingRight)
            spawnPosition += Vector3.right * 0.5f;
        else
            spawnPosition += Vector3.left * 0.5f;

        GameObject projectile = Instantiate(arcaneProjectilePrefab, spawnPosition, isFacingRight ? Quaternion.identity : Quaternion.Euler(0, 180, 0));
        ArcaneProjectile projectileComponent = projectile.GetComponent<ArcaneProjectile>();

        if (projectileComponent != null)
        {
            projectileComponent.SetDirection(isFacingRight);
            projectileComponent.damage = arcana * 5;
        }
        else
            Debug.LogError("ArcaneProjectile script not found on the projectile prefab!");
    }

    public void FixHealth()
    {
        health = maxHealth;
        healthFixed = true;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        coyoteTimeCounter = 0;
    }

    public void AddStrength()
    {
        if (FindObjectOfType<GoldManager>().gold >= levelUpCost)
        {
            strength++;
            FindObjectOfType<GoldManager>().RemoveGold(levelUpCost);
            LevelUp();
        }
    }

    public void AddArcana()
    {
        if (FindObjectOfType<GoldManager>().gold >= levelUpCost)
        {
            arcana++;
            FindObjectOfType<GoldManager>().RemoveGold(levelUpCost);
            LevelUp();
        }
    }

    public void AddAgility()
    {
        if (FindObjectOfType<GoldManager>().gold >= levelUpCost)
        {
            agility++;
            FindObjectOfType<GoldManager>().RemoveGold(levelUpCost);
            LevelUp();
        }
    }

    public void AddVitality()
    {
        if (FindObjectOfType<GoldManager>().gold >= levelUpCost)
        {
            vitality++;
            FindObjectOfType<GoldManager>().RemoveGold(levelUpCost);
            LevelUp();
        }
    }

    public int GetPlayerLevel() => playerLevel;
    public int GetStrength() => strength;
    public int GetArcana() => arcana;
    public float GetAgility() => agility;
    public int GetVigor() => vitality;
    public int GetLevelUpCost() => levelUpCost;

    void Attack()
    {
        Vector3 spawnPosition;
        bool holdingUp = Input.GetKey(KeyCode.W);
        bool holdingDown = Input.GetKey(KeyCode.S);
        bool isAirborne = !isGrounded;

        if (holdingUp)
            spawnPosition = transform.position + Vector3.up * attackOffset;
        else if (holdingDown && isAirborne)
            spawnPosition = transform.position + Vector3.down * attackOffset;
        else
            spawnPosition = transform.position + (isFacingRight ? Vector3.right : Vector3.left) * attackOffset;

        GameObject attack = Instantiate(attackPrefab, spawnPosition, Quaternion.identity);

        Collider2D attackCollider = attack.GetComponent<Collider2D>();
        if (attackCollider != null)
            attackCollider.isTrigger = true;
        else
            Debug.Log("no collider");

        AttackBehavior attackBehavior = attack.GetComponent<AttackBehavior>();
        if (attackBehavior == null)
            attackBehavior = attack.AddComponent<AttackBehavior>();

        if (isGrounded)
            StartCoroutine(AttackLock());
    }

    IEnumerator AttackLock()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        hasIFrames = true;
        rb.velocity = new Vector2((isFacingRight ? 1 : -1) * dashForce, 0);
        int originalLayerMask = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
        yield return new WaitForSeconds(dashDuration);
        gameObject.layer = originalLayerMask;
        rb.velocity = Vector2.zero;
        isDashing = false;
        hasIFrames = false;

        dashCooldownTimer = dashCooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    void ProcessHit(DamageDealer damageDealer)
    {
        if (!hasIFrames)
        {
            health -= damageDealer.GetDamage() - strength * 2;
            if (health <= 0)
                Debug.Log("Player dead");
        }
    }

    public void LevelUp()
    {
        playerLevel++;
    }
}

public class AttackBehavior : MonoBehaviour
{
    public float lifetime = 0.1f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}