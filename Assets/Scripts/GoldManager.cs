using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int currentGold = 0;

    void Start()
    {
        currentGold = 0;
        Debug.Log("Starting gold: " + currentGold);
    }
    public void AddGold(int amount)
    {
        currentGold += amount;
        Debug.Log("Gold Added! Current Gold: " + currentGold);
    }
    public void SubtractGold(int amount)
    {
        currentGold = Mathf.Max(currentGold - amount, 0);
        Debug.Log("Gold Subtracted! Current Gold: " + currentGold);
    }
    public int GetCurrentGold()
    {
        return currentGold;
    }
}
