using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    public float lifetime = 0.1f;
    PlayerController player;

    private void Awake()
    {
        gameObject.GetComponent<DamageDealer>().damage = FindObjectOfType<PlayerController>().strength * 2;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (player.isFiery)
            {
                StartCoroutine(enemy.SwordOnFire());
            }
            if (player.isCold)
            {
                StartCoroutine(enemy.SwordCold());
            }
            if (player.isShocking)
            {
                StartCoroutine(enemy.SwordLightning());
            }
            if (player.isCursed)
            {
                StartCoroutine(enemy.SwordCursed());
            }

            enemy.HitAnimation();
        }

        Destroy(gameObject);
    }
}
