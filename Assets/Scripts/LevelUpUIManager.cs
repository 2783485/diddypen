using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HideLevelUpUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HideLevelUpUI()
    {
        gameObject.SetActive(false);
    }
    public void ShowLevelUpUI()
    {
        gameObject.SetActive(true);
    }
    public void LevelUpStrength()
    {
        FindObjectOfType<PlayerController>().AddStrength();
    }
    public void LevelUpArcana()
    {
        FindObjectOfType<PlayerController>().AddArcana();
    }
    public void LevelUpAgility()
    {
        FindObjectOfType<PlayerController>().AddAgility();
    }
    public void LevelUpVigor()
    {
        FindObjectOfType<PlayerController>().AddVitality();
    }
}
