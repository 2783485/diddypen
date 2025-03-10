using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float postCombatCooldown = 5f;
    public bool hasSpawnedEnemy = false;
    private GameObject activeEnemy;
    private bool inPostCombat = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TrySpawnEnemy();
        }
    }

    void TrySpawnEnemy()
    {
        if (inPostCombat)
        {

            return;
        }

        if (activeEnemy != null)
        {
            return;
        }

        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (hasSpawnedEnemy) return; 

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().SetSpawnHandler(this); 
        hasSpawnedEnemy = true;
    }

    public void EnemyDead()
    {
        activeEnemy = null;
        EnterPostCombatState();
    }

    void EnterPostCombatState()
    {
        inPostCombat = true;
        Invoke(nameof(ExitPostCombatState), postCombatCooldown);
    }

    void ExitPostCombatState()
    {
        inPostCombat = false;
    }
}

