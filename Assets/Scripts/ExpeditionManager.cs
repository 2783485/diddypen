using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ExpeditionManager : MonoBehaviour
{
    public int workers;
    public int roundsPassed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddWorker()
    {
        workers++;
    }
    public void RemoveWorker()
    {
        workers--;
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
        Expedition();
    }
    public void Expedition()
    {
        if (roundsPassed >= 5)
        {
            for (int i = 0; i < workers; i++)
            {
                
            }
        }
    }
}
