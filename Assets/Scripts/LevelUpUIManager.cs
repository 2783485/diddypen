using UnityEngine;
using UnityEngine.UI;

public class LevelUpUIManager : MonoBehaviour
{
    public GameObject levelUpScreen;
    public Button continueButton;

    private void Start()
    {
        levelUpScreen.SetActive(false);
        continueButton.onClick.AddListener(HideLevelUpScreen);
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
}
