using UnityEngine;
using System.Collections.Generic;

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

    public int workerCount = 0;
    public int workerHireCost = 10;
    public int maxExpeditions = 3;
    public List<Expedition> activeExpeditions = new List<Expedition>();

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
        gold += goldDrop;
        currentPhase = CombatPhase.PostCombat; 
        CheckExpeditions();
        Debug.Log("Combat phase ended. Entering post-combat state.");
    }
    public int GetWorkers()
    {
        return workerCount;
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

    public void HireWorker()
    {
        int cost = workerHireCost + (workerCount * 10);
        if (gold >= cost)
        {
            gold -= cost;
            workerCount++;
            Debug.Log("Worker hired! Total workers: " + workerCount);
        }
        else
        {
            Debug.Log("Not enough gold to hire a worker!");
        }
    }

    public void StartExpedition(int workersAssigned, int duration)
    {
        if (activeExpeditions.Count >= maxExpeditions)
        {
            Debug.Log("Max expeditions reached!");
            return;
        }

        if (workersAssigned > workerCount)
        {
            Debug.Log("Not enough workers available!");
            return;
        }

        workerCount -= workersAssigned;
        Expedition newExpedition = new Expedition(workersAssigned, duration + combatPhase);
        activeExpeditions.Add(newExpedition);

        Debug.Log("Expedition started with " + workersAssigned + " workers for " + duration + " combat phases.");
    }

    public void CheckExpeditions()
    {
        List<Expedition> completedExpeditions = new List<Expedition>();

        foreach (var expedition in activeExpeditions)
        {
            if (combatPhase >= expedition.completionPhase)
            {
                gold += Random.Range(5, 20);
                Debug.Log("Expedition completed! Gained gold and materials.");
                completedExpeditions.Add(expedition);
            }
        }

        foreach (var expedition in completedExpeditions)
        {
            activeExpeditions.Remove(expedition);
            workerCount += expedition.workers;
        }
    }
}

public class Expedition
{
    public int workers;
    public int completionPhase;

    public Expedition(int workers, int completionPhase)
    {
        this.workers = workers;
        this.completionPhase = completionPhase;
    }
}
