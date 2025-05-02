using UnityEngine;

public class ArcaneProjectile : MonoBehaviour
{
    public float speed = 10f;
    public bool isFacingRight;
    private Rigidbody2D rb;
    public float lifetime = 2f;
    PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        rb.velocity = isFacingRight ? Vector2.right * speed : Vector2.left * speed;
    }

    public void SetDirection(bool facingRight)
    {
        isFacingRight = facingRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (player.isCursedProj)
            {
                StartCoroutine(enemy.ProjCursed());
            }
            if (player.isLightningProj)
            {
                StartCoroutine(enemy.ProjLightning());
            }
            if (player.isColdProj)
            {
                StartCoroutine(enemy.ProjCold());
            }
            if (player.isFieryProj)
            {
                StartCoroutine(enemy.ProjOnFire());
            }

            enemy.HitAnimation();
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        GetComponent<DamageDealer>().damage = FindObjectOfType<PlayerController>().arcana * 7;
    }
}
