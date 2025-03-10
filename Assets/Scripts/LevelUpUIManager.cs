using UnityEngine;
using UnityEngine.UI;

public class LevelUpUIManager : MonoBehaviour
{
    public GameObject levelUpScreen;
    public GameObject expeditionMenu;
    public Button continueButton;
    public Button expeditionButton;
    //public Button strUpButton;
    //public Button intUpButton;
    //public Button vitUpButton;

    private void Start()
    {
        expeditionMenu.SetActive(false);
        levelUpScreen.SetActive(false);
        continueButton.onClick.AddListener(HideLevelUpScreen);
        expeditionButton.onClick.AddListener(() => GameManager.Instance.StartExpedition(3, 2));
    }
    public void ShowLevelUpScreen()
    {
        levelUpScreen.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void HideLevelUpScreen()
    {
        levelUpScreen.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ShowExpeditionMenu()
    {
        expeditionMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void HideExpeditionMenu()
    {
        expeditionMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
