using UnityEngine;

public class ArcaneProjectile : MonoBehaviour
{
    public float speed = 10f;
    public bool isFacingRight;
    public int damage = 10;
    private Rigidbody2D rb;
    public float lifetime = 2f;

    void Start()
    {
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy Attack"))
        {
            Destroy(gameObject);
        }
    }
}