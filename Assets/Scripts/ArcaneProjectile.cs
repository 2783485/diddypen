using UnityEngine;

public class ArcaneProjectile : MonoBehaviour
{
    public float speed = 10f;
    public bool isFacingRight;
    private Rigidbody2D rb;
    public float lifetime = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }
    private void Update()
    {
        gameObject.GetComponent<DamageDealer>().damage = FindObjectOfType<PlayerController>().arcana * 7;
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
        Destroy(gameObject);
    }
    
}