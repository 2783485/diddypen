using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int mushrooms;
    public int arcanaOrb;
    public int emptyBottles;
    public int healthPot;
    public int projectileSkill;
    public int skillBuffPot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CraftHealthPot()
    {
        if (mushrooms > 0 && emptyBottles > 0)
        {
            mushrooms--;
            emptyBottles--;
            healthPot += 2;
        }
    }
    public void CraftProjectile()
    {
        if (mushrooms > 0 && arcanaOrb > 0)
        {
            mushrooms--;
            arcanaOrb--;
            projectileSkill += 4;
        }
    }
    public void CraftBuffPot()
    {
        if (arcanaOrb > 0 && emptyBottles > 0)
        {
            emptyBottles--;
            arcanaOrb--;
            skillBuffPot++;
        }
    }
}