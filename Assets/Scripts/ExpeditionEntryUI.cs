using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpeditionEntryUI : MonoBehaviour
{
    public TextMeshProUGUI workersText;
    public TextMeshProUGUI durationText;
    public TextMeshProUGUI timeLeftText;
    public Button addWorkerButton;
    public Button removeWorkerButton;
    public Button addTimeButton;
    public Button removeTimeButton;

    private int expeditionIndex;

    // Setup the UI for each expedition entry
    //public void Setup(Expedition expedition, int index)
    //{
        //expeditionIndex = index;
        //workersText.text = $"Workers: {expedition.workers}";
        //durationText.text = $"Rounds To Complete: {expedition.completionPhase - GameManager.Instance.combatPhase}";
        //imeLeftText.text = $"Rounds Left: {expedition.completionPhase - GameManager.Instance.combatPhase}";

        // Add listeners to buttons
        //addWorkerButton.onClick.AddListener(() => ExpeditionManager.Instance.AddWorker(expeditionIndex));
        //removeWorkerButton.onClick.AddListener(() => ExpeditionManager.Instance.RemoveWorker(expeditionIndex));
        //a//ddTimeButton.onClick.AddListener(() => ExpeditionManager.Instance.AddRounds(expeditionIndex));
        //removeTimeButton.onClick.AddListener(() => ExpeditionManager.Instance.RemoveRounds(expeditionIndex));
    //}
}
