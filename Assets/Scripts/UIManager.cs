using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject levelUpPanel;
    public GameObject expeditionPanel;
    public int currentOpenUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowLevelUpPanel()
    {
        if (currentOpenUI == 0)
        {
            levelUpPanel.SetActive(true);
            currentOpenUI = 1;
        }
    }
    public void HideLevelUpPanel()
    {
        currentOpenUI = 0;
        levelUpPanel.SetActive(false);
    }
    public void ShowExpeditionPanel()
    {
        if (currentOpenUI == 0)
        {
            expeditionPanel.SetActive(true);
            currentOpenUI = 2; 
        }
    }
    public void HideExpeditionPanel()
    {
        expeditionPanel.SetActive(false);
        currentOpenUI = 0;
    }
}
