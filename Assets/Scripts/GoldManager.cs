using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int gold;
    void Start()
    {
        
    }
    void Update()
    {
        GetGold();
    }
    public void AddGold()
    {
        gold += FindObjectOfType<Enemy>().GetGoldDropped();
    }
    public int GetGold()
    {
        return gold;    
    }
}
