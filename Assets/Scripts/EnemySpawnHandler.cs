using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float postCombatCooldown = 5f;

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

    void SpawnEnemy()
    {
        activeEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        activeEnemy.GetComponent<Enemy>().SetSpawnHandler(this);
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

