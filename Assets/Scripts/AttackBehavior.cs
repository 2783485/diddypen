using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    public float lifetime = 0.1f;

    private void Awake()
    {
        gameObject.GetComponent<DamageDealer>().damage = FindObjectOfType<PlayerController>().strength * 2;
    }
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    private void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<Enemy>().HitAnimation();
        Destroy(gameObject);
    }
}