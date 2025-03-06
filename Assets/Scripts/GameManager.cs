using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject levelUpScreen;
    public int gold;
    public int goldNeededForLevelUp = 20;
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
        Debug.Log(gold);
    }
    public void TriggerLevelUpScreen()
    {
        FindObjectOfType<LevelUpUIManager>().ShowLevelUpScreen();
    }
    public void AddGold()
    {
        gold += FindObjectOfType<Enemy>().GetGoldDrop();
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
}
