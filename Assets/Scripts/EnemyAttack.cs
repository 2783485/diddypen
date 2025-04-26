using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<DamageDealer>().damage = FindObjectOfType<Enemy>().damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<DamageDealer>().damage = FindObjectOfType<Enemy>().damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<PlayerController>().HitAnimation();
    }
}
