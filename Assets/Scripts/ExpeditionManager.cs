using UnityEngine;

public class ExpeditionManager : MonoBehaviour
{
    public static ExpeditionManager Instance;

    public int availableWorkers = 0;  // Starts at 0 workers
    public int workersSentOnExpedition = 0;  

    private const int maxWorkers = 6;
    private const int workerCost = 20;  // 20 gold per worker

    public bool isExpeditionActive = false;  // Tracks whether an expedition is active
    private bool expeditionComplete = false;  // Tracks if expedition is complete
    public int gold;

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
    private void Update()
    {
        gold = FindObjectOfType<GameManager>().gold;
    }

    public void AddWorker()
    {
        // Ensure that we don't exceed max workers and have enough gold
        if (availableWorkers < maxWorkers && gold >= workerCost)
        {
            availableWorkers++;
            gold -= workerCost;
            Debug.Log($"Worker hired! Total Workers: {availableWorkers}, Gold left: {gold}");
        }
        else
        {
            Debug.Log("Cannot hire more workers. Either you've reached the max limit or don't have enough gold.");
        }
    }

    public void StartExpedition()
    {
        // Start the expedition if we have workers and it is not already active
        if (workersSentOnExpedition > 0 && !isExpeditionActive)
        {
            isExpeditionActive = true;
            expeditionComplete = false;
            Debug.Log($"Expedition started with {workersSentOnExpedition} workers!");
            // Additional logic for starting the expedition
        }
        else if (workersSentOnExpedition == 0)
        {
            Debug.Log("You need to send at least 1 worker to start the expedition.");
        }
        else if (isExpeditionActive)
        {
            Debug.Log("Expedition is already active.");
        }
    }

    public void ContinueGame()
    {
        // Logic for continuing the game (e.g., unpausing, going back to the main game scene, etc.)
        Debug.Log("Returning to game...");
    }

    public void SendWorkersOnExpedition(int workers)
    {
        if (workers <= availableWorkers)
        {
            workersSentOnExpedition = workers;
            availableWorkers -= workers;
            Debug.Log($"{workers} workers sent on the expedition.");
        }
        else
        {
            Debug.Log("Not enough workers available.");
        }
    }

    public void EndExpedition()
    {
        if (isExpeditionActive)
        {
            // Workers are removed after the expedition
            availableWorkers += workersSentOnExpedition;
            workersSentOnExpedition = 0;
            isExpeditionActive = false;
            expeditionComplete = true;
            Debug.Log($"Expedition complete. Workers returned.");
        }
    }

    public bool IsExpeditionComplete()
    {
        return expeditionComplete;
    }
}
