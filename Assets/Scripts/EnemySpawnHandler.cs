using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
    public GameObject enemy;
    bool enemySpawned;

    private void Update()
    {
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        if (!enemySpawned)
        {
            enemySpawned = true;
            Instantiate(enemy);
        }
    }
    public void EnemyDead()
    {
        enemySpawned = false;
    }
}
