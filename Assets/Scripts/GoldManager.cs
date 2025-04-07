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
    public void AddGold(int amount)
    {
        gold += amount;
    }
    public void RemoveGold(int amountLost)
    {
        gold -= amountLost;
    }
    public int GetGold()
    {
        return gold;    
    }
}
