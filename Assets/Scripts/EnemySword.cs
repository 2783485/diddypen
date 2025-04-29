using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        PlaySwing();
        Destroy(gameObject, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySwing()
    {
        if (FindObjectOfType<Enemy>().enemyAttackType == 1)
        {
            anim.Play("EnemySwingUp");
        }
        else if (FindObjectOfType<Enemy>().enemyAttackType == 2)
        {
            anim.Play("EnemySwingRight");
        }
        else if (FindObjectOfType<Enemy>().enemyAttackType == 3)
        {
            anim.Play("EnemySwingDown");
        }
        else if (FindObjectOfType<Enemy>().enemyAttackType == 4)
        {
            anim.Play("EnemySwingLeft");
        }
    }
}

