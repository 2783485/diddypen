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
        roundsLeft = 5 - roundsPassed;  
        goldCost = 25 + workers * 5;
        if (expeditionStarted && !expeditionEnded)
        {
            Expedition();
            roundsPassed = 0;
            workers = 0;
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
        if (!expeditionStarted)
        {
            expeditionStarted = true;
        }
    }
    public void Expedition()
    {
        if (roundsPassed >= 5)
        {
            expeditionEnded = true;
            expeditionStarted = false;
            for (int i = 0; i < workers; i++)
            {
                int itemRoll = Random.Range(0, 20);
                if (itemRoll == 1 || itemRoll == 2)
                {
                    FindObjectOfType<InventoryManager>().mushrooms++;
                }
                if (itemRoll == 3 || itemRoll == 4)
                {
                    FindObjectOfType<InventoryManager>().arcanaOrb++;
                }
                if(itemRoll == 5 || itemRoll == 6)
                {
                    FindObjectOfType<InventoryManager>().emptyBottles++;
                }
                if(itemRoll == 7 || itemRoll == 8)
                {
                    FindObjectOfType<InventoryManager>().healthPot++;
                }
            }
        }
    }
}
