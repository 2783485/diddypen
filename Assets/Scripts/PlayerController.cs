using UnityEngine;
using System.Collections;
using Unity.PlasticSCM.Editor.WebApi;

public class PlayerController : MonoBehaviour
{
    public GameObject swordPrefab;
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
    public float fireRate;
    public int attackType;
    bool buffedUp = false;
    int drankOnRound;
    bool unbuffed;
    public ParticleSystem bloodVFX;
    public Sprite playerHit;
    public Sprite playerDefault;
    public ParticleSystem bloodExplosionVFX;
    public GameObject playerCorpse;
    public bool hitStopOn;

    bool isAttacking = false;
    Rigidbody2D rb;
    bool isGrounded;
    float coyoteTimeCounter;
    float defaultGravityScale;
    public bool isFacingRight = true;
    bool canFire = true;
    Collider2D coli;

    void Start()
    {
        Instantiate(attackPrefab, transform.position, Quaternion.identity);
        rb = GetComponent<Rigidbody2D>();
        coli = GetComponent<Collider2D>();
        defaultGravityScale = rb.gravityScale;
        rb.freezeRotation = true;
        maxHealth = 40 + vitality * 5;
        levelUpCost = 20 + playerLevel * 10;
        FixHealth();
    }

    void Update()
    {
        levelUpCost = 10 + playerLevel * 5;
        if (!isAttacking)
        {
            Move();
        }
        else if (isAttacking)
        {
            rb.velocity = Vector2.zero;
        }

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
            {
                rb.gravityScale = fastFallGravityScale;
            }
        }

        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0)
        {
            Jump();
        }
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && attackCooldownTimer <= 0)
        {
            Attack();
            attackCooldownTimer = 1f / attackCooldown;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(Fire());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0)
        {
            StartCoroutine(Dash());
        }
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            HealPotion();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            BuffPotion();
        }
        if (FindObjectOfType<RoundManager>().roundsPassed - drankOnRound >= 2)
        {
            if (!unbuffed && buffedUp)
            {
                Unbuff();
            }
            unbuffed = true;
            buffedUp = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<UIManager>().ShowMainPanel();
        }

    }

    void Move()
    {
        if (isDashing)
        {
            return;
        }

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }
    }

    IEnumerator Fire()
    {
        if (canFire)
        {
            canFire = false;
            if (FindObjectOfType<InventoryManager>().projectileSkill > 0)
            {
                FindObjectOfType<InventoryManager>().projectileSkill--;
                Vector3 spawnPosition = transform.position;
                if (isFacingRight)
                {
                    spawnPosition += Vector3.right * 0.5f;
                }
                else
                {
                    spawnPosition += Vector3.left * 0.5f;
                }

                GameObject projectile = Instantiate(arcaneProjectilePrefab, spawnPosition, isFacingRight ? Quaternion.identity : Quaternion.Euler(0, 180, 0));
                ArcaneProjectile projectileComponent = projectile.GetComponent<ArcaneProjectile>();

                if (projectileComponent != null)
                {
                    projectileComponent.SetDirection(isFacingRight);
                }
                yield return new WaitForSeconds(fireRate);
                canFire = true;
            }
        }
    }

    public void FixHealth()
    {
        health = maxHealth;
        healthFixed = true;
    }

    public void Jump()
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
            maxHealth = 10 + vitality * 5;
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
        {
            spawnPosition = transform.position + Vector3.up * attackOffset;
            attackType = 1;
        }
        else if (holdingDown && isAirborne)
        {
            spawnPosition = transform.position + Vector3.down * attackOffset;
            attackType = 3;
        }
        else
        {
            spawnPosition = transform.position + (isFacingRight ? Vector3.right : Vector3.left) * attackOffset;
            if (isFacingRight)
            {
                attackType = 2;
            }
            else if (!isFacingRight)
            {
                attackType = 4;
            }
        }
        GameObject attack = Instantiate(attackPrefab, spawnPosition, Quaternion.identity);
        Instantiate(swordPrefab, spawnPosition, Quaternion.identity);
        if (!isFacingRight)
        {
            Vector3 scale = attack.transform.localScale;
            scale.x *= -1;
            attack.transform.localScale = scale;
        }
        Collider2D attackCollider = attack.GetComponent<Collider2D>();
        if (attackCollider != null)
        {
            attackCollider.isTrigger = true;
        }
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
            StartCoroutine(HitStop());
            health -= damageDealer.GetDamage();
            if (health <= 0)
            {
                Die();
            }
        }
    }
    public void Die()
    {
        health = 0;
        GetComponent<SpriteRenderer>().sprite = playerHit;
        moveSpeed = 0;
        jumpForce = 0;
        dashDuration = 0;
        dashForce = 0;
        Instantiate(playerCorpse, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.05f);
    }
    public IEnumerator HitStop()
    {
        hitStopOn = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.05f);
        Time.timeScale = 1f;
        hitStopOn = false;
    }
    public void HitAnimation()
    {
        StartCoroutine(HitSprites());
    }
    public IEnumerator HitSprites()
    {
        Instantiate(bloodVFX, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().sprite = playerHit;
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().sprite = playerDefault;
    }

    public void LevelUp()
    {
        playerLevel++;
    }
    public void BuffPotion()
    {
        if (!buffedUp)
        {
            drankOnRound = FindObjectOfType<RoundManager>().roundsPassed;
            FindObjectOfType<InventoryManager>().skillBuffPot--;
            Buffed();
            buffedUp = true;
        }
    }
    public void Buffed()
    {
        strength += strength/2;
        arcana += strength/2;
        agility += strength/2;
        vitality += strength/ 2;
        maxHealth = 40 + vitality * 5;
    }
    public void Unbuff()
    {
        strength -= 5;
        agility -= 5;
        arcana -= 5;
        vitality -= 5;
        maxHealth = 40 + vitality * 5;
    }
    public void HealPotion()
    {
        if (FindObjectOfType<InventoryManager>().healthPot > 0)
        {
            FindObjectOfType<InventoryManager>().healthPot--;
            health += vitality / 2;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
    }
}
