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
                enemy.StartFireSword();
            }
            if (player.isCold)
            {
                enemy.ColdSword();
            }
            if (player.isShocking)
            {
                enemy.LightningSword();
            }
            if (player.isCursed)
            {
                enemy.CursedSword();
            }

            enemy.HitAnimation();
        }

        Destroy(gameObject);
    }
}
