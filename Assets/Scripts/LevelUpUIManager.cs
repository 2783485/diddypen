using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
