using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject levelUpPanel;
    public GameObject expeditionPanel;
    public GameObject inventoryPanel;
    public GameObject mainUI;
    public GameObject lootBoxUI;
    public GameObject forgePanel;
    public GameObject[] statIcons;
    public int currentOpenUI;
    // Start is called before the first frame update
    void Start()
    {
        mainUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentOpenUI == 0)
        {
            foreach (GameObject obj in statIcons)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
        else
        {
            foreach (GameObject obj in statIcons)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
    public void ShowLevelUpPanel()
    {
        if (currentOpenUI == 0 || currentOpenUI == 4)
        {
            Time.timeScale = 0f;
            levelUpPanel.SetActive(true);
            mainUI.SetActive(false);
            currentOpenUI = 1;
        }
    }
    public void HideLevelUpPanel()
    {
        Time.timeScale = 1f;
        currentOpenUI = 0;
        levelUpPanel.SetActive(false);
    }
    public void ShowExpeditionPanel()
    {
        if (currentOpenUI == 0 || currentOpenUI == 4)
        {
            Time.timeScale = 0f;
            expeditionPanel.SetActive(true);
            mainUI.SetActive(false);
            currentOpenUI = 2; 
        }
    }
    public void HideExpeditionPanel()
    {
        Time.timeScale = 1f;
        expeditionPanel.SetActive(false);
        currentOpenUI = 0;
    }
    public void ShowInventoryPanel()
    {
        if (currentOpenUI == 0 || currentOpenUI == 4)
        {
            Time.timeScale = 0f;
            inventoryPanel.SetActive(true);
            mainUI.SetActive(false);
            currentOpenUI = 3;
        }
    }
    public void HideInventoryPanel()
    {
        Time.timeScale = 1f;
        inventoryPanel.SetActive(false);
        currentOpenUI = 0;
    }
    public void ShowLootboxPanel()
    {
        if (currentOpenUI == 0 || currentOpenUI == 4)
            Time.timeScale = 0f;
            lootBoxUI.SetActive(true);
            mainUI.SetActive(false);
            currentOpenUI = 5;
    }
    public void ShowForgePanel()
    {
        if (currentOpenUI == 0 || currentOpenUI == 4)
            Time.timeScale = 0f;
            forgePanel.SetActive(true);
            mainUI.SetActive(false);
            currentOpenUI = 6;
    }
    public void HideForgePanel()
    {
        Time.timeScale = 1f;
        forgePanel.SetActive(false);
        currentOpenUI = 0;
    }
    public void HideLootboxPanel()
    {
        Time.timeScale = 1f;
        lootBoxUI.SetActive(false);
        currentOpenUI = 0;
    }
    public void ShowMainPanel()
    {
        if (currentOpenUI == 0)
        {
            Time.timeScale = 0f;
            mainUI.SetActive(true);
            currentOpenUI = 4;
        }
    }
    public void HideMainPanel()
    {
        Time.timeScale = 1f;
        mainUI.SetActive(false);
        currentOpenUI = 0;
    }
}
