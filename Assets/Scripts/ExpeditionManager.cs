using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ExpeditionManager : MonoBehaviour
{
    public int workers;
    public int roundsPassed;
    public int roundsLeft;
    public int goldCost;
    public bool expeditionStarted;
    public bool expeditionEnded;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        roundsLeft = 3 - roundsPassed;  
        goldCost = 10 + workers * 5;
        if (expeditionStarted && !expeditionEnded)
        {
            Expedition();
        }
    }
    public void AddWorker()
    {
        if (FindObjectOfType<GoldManager>().gold >= goldCost)
        {
            workers++;
            FindObjectOfType<GoldManager>().RemoveGold(goldCost);
        }
    }
    public void RemoveWorker()
    {
        if (workers > 0)
        {
            workers--;
            FindObjectOfType<GoldManager>().AddGold(goldCost);
        }
    }
    public int ReturnWorkers() { return workers; }
    public void AddRoundPassed()
    {
        roundsPassed++;
    }
    public void RemoveRoundPassed()
    {
        roundsPassed--;
    }
    public void ResetRoundsPassed()
    {
        roundsPassed = 0;
    }
    public int ReturnRoundsPassed() {  return roundsPassed; }
    public void StartExpedition()
    {
        if (!expeditionStarted && workers > 0)
        {
            expeditionStarted = true;
            expeditionEnded = false;
        }
    }
    public void Expedition()
    {
        if (roundsPassed >= 3)
        {
            for (int i = 0; i < workers; i++)
            {
                int itemRoll = Random.Range(0, 21);
                if (itemRoll == 1 || itemRoll == 2)
                {
                    FindObjectOfType<InventoryManager>().mushrooms += 2;
                }
                if (itemRoll == 3 || itemRoll == 4)
                {
                    FindObjectOfType<InventoryManager>().arcanaOrb += 2;
                }
                if (itemRoll == 5 || itemRoll == 6)
                {
                    FindObjectOfType<InventoryManager>().emptyBottles += 2;
                }
                if (itemRoll == 7 || itemRoll == 8)
                {
                    FindObjectOfType<InventoryManager>().healthPot++;
                }
                if (itemRoll == 9 || itemRoll == 10 || itemRoll == 11 || itemRoll == 12)
                {
                    FindObjectOfType<InventoryManager>().mushrooms += 4;
                    FindObjectOfType<InventoryManager>().arcanaOrb += 2;
                }
                if (itemRoll == 13)
                {
                    FindObjectOfType<InventoryManager>().treasureChests++;
                }
            }
            expeditionEnded = true;
            expeditionStarted = false;
            roundsPassed = 0;
            workers = 0;
        }
    }
}
