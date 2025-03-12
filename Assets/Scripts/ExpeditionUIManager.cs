using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpeditionUIManager : MonoBehaviour
{
    public TextMeshProUGUI workerText;  // Displays the number of workers hired
    public TextMeshProUGUI goldText;     // Displays the available gold
    public TextMeshProUGUI workersSentText;  // Displays the number of workers sent on the expedition

    public Button addWorkerButton;      // Button to hire a worker
    public Button startExpeditionButton;  // Button to start the expedition
    public Button continueButton;        // Button to continue the game

    private void Start()
    {
        // Initialize UI elements
        UpdateUI();
    }

    private void Update()
    {
        // Continuously update the UI with the latest values
        UpdateUI();
    }

    private void UpdateUI()
    {
        workerText.text = $"Workers: {ExpeditionManager.Instance.availableWorkers}";
        workersSentText.text = $"Workers Sent: {ExpeditionManager.Instance.workersSentOnExpedition}";

        // Disable Add Worker button if there's not enough gold or max workers are reached
        addWorkerButton.interactable = ExpeditionManager.Instance.gold >= 20 && ExpeditionManager.Instance.availableWorkers < 6;

        // Disable Start Expedition button if no workers are sent or the expedition is already active
        startExpeditionButton.interactable = ExpeditionManager.Instance.workersSentOnExpedition > 0 && !ExpeditionManager.Instance.isExpeditionActive;
    }

    public void OnAddWorkerButton()
    {
        ExpeditionManager.Instance.AddWorker();
    }

    public void OnStartExpeditionButton()
    {
        ExpeditionManager.Instance.StartExpedition();
    }

    public void OnContinueButton()
    {
        ExpeditionManager.Instance.ContinueGame();
    }
}
