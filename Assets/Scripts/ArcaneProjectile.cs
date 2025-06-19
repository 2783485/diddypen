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
                enemy.CursedProj();
            }
            if (player.isLightningProj)
            {
                enemy.LightningProj();
            }
            if (player.isColdProj)
            {
                enemy.StartProjCold();
            }
            if (player.isFieryProj)
            {
                enemy.FireProj();
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
