using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject levelUpScreen;
    public int gold;
    public int goldNeededForLevelUp = 20;

    public int combatPhase = 0;
    public bool isPostCombat = false;
    public enum CombatPhase { Combat, PostCombat }
    public CombatPhase currentPhase = CombatPhase.Combat;

    public int workerHireCost = 10;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold added: " + amount + " | Total Gold: " + gold);
    }

    public void IncreaseStrength()
    {
        if (gold >= goldNeededForLevelUp)
        {
            FindObjectOfType<PlayerController>().AddStrength();
            gold -= goldNeededForLevelUp;
            goldNeededForLevelUp += 5;
        }
    }

    public void IncreaseIntelligence()
    {
        if (gold >= goldNeededForLevelUp)
        {
            FindObjectOfType<PlayerController>().AddIntelligence();
            gold -= goldNeededForLevelUp;
            goldNeededForLevelUp += 5;
        }
    }

    public void IncreaseVitality()
    {
        if (gold >= goldNeededForLevelUp)
        {
            FindObjectOfType<PlayerController>().AddVitality();
            gold -= goldNeededForLevelUp;
            goldNeededForLevelUp += 5;
        }
    }

    public void OnEnemyDeath(int goldDrop)
    {
        AddGold(goldDrop);
        isPostCombat = true;
        currentPhase = CombatPhase.PostCombat;

        //ExpeditionManager.Instance.CheckExpeditions(); // Check expeditions here

        Debug.Log("Combat phase ended. Entering post-combat state.");
    }

    public void StartNewCombatPhase()
    {
        if (!isPostCombat)
            return;

        combatPhase++;
        isPostCombat = false;

        Debug.Log("Starting Combat Phase: " + combatPhase);
        currentPhase = CombatPhase.Combat;
    }

    public bool IsPostCombat()
    {
        return currentPhase == CombatPhase.PostCombat;
    }

    public void TriggerLevelUpScreen()
    {
        FindObjectOfType<LevelUpUIManager>().ShowLevelUpScreen();
    }
}
