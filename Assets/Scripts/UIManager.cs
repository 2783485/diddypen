using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject levelUpPanel;
    public GameObject expeditionPanel;
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
        levelUpPanel.SetActive(true);
    }
    public void HideLevelUpPanel()
    {
        levelUpPanel.SetActive(false);
    }
    public void ShowExpeditionPanel()
    {
        expeditionPanel.SetActive(true);
    }
    public void HideExpeditionPanel()
    {
        expeditionPanel.SetActive(false);
    }
}
