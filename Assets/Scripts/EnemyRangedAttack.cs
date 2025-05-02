using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{

    private void Awake()
    {
        gameObject.GetComponent<DamageDealer>().damage = FindObjectOfType<Enemy>().damage / 2;
    }
    void Start()
    {
        
    }
    private void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<PlayerController>().HitAnimation();
        Destroy(gameObject);
    }
}