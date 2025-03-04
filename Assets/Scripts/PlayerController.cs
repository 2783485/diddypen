using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float fastFallGravityScale = 4f;
    public float coyoteTime = 0.1f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public GameObject attackPrefab;
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
    }

    void Update()
    {
        if (!isAttacking)
        {
            Move();
        }
        else if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }

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

        if (Input.GetButtonDown("Fire2") && dashCooldownTimer <= 0)
        {
            StartCoroutine(Dash());
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Move()
    {
        if (isDashing) return;

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

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        coyoteTimeCounter = 0;
    }

    void Attack()
    {
        Vector3 spawnPosition;
        bool holdingUp = Input.GetKey(KeyCode.W);
        bool holdingDown = Input.GetKey(KeyCode.S);
        bool isAirborne = !isGrounded;

        if (holdingUp)
        {
            spawnPosition = transform.position + Vector3.up * attackOffset;
        }
        else if (holdingDown && isAirborne)
        {
            spawnPosition = transform.position + Vector3.down * attackOffset;
        }
        else
        {
            spawnPosition = transform.position + (isFacingRight ? Vector3.right : Vector3.left) * attackOffset;
        }

        GameObject attack = Instantiate(attackPrefab, spawnPosition, Quaternion.identity);

        Collider2D attackCollider = attack.GetComponent<Collider2D>();
        if (attackCollider != null)
        {
            attackCollider.isTrigger = true;
        }
        else
        {
            Debug.Log("no collider");
        }

        AttackBehavior attackBehavior = attack.GetComponent<AttackBehavior>();
        if (attackBehavior == null)
        {
            attackBehavior = attack.AddComponent<AttackBehavior>();
        }

        if (isGrounded)
        {
            StartCoroutine(AttackLock());
        }
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
        coli.enabled = false;

        yield return new WaitForSeconds(dashDuration);

        coli.enabled = true;
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
        health -= damageDealer.GetDamage();
        if (health <= 0) { Debug.Log("Player dead"); }
    }

}

public class AttackBehavior : MonoBehaviour
{
    public float lifetime = 0.1f;
    public int damage = 10;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
