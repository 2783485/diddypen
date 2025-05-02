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
    public int treasureChests;
    public int ruby;
    public int sapphire;
    public int topaz;
    public int blackDiamond;
    public ParticleSystem goldParticles;
    int treasureChestClicks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (treasureChestClicks >= 10)
        {
            OpenLootBox();
            treasureChestClicks = 0;
        }
        
    }
    public void CraftHealthPot()
    {
        if (mushrooms >= 1 && emptyBottles >= 2)
        {
            mushrooms--;
            emptyBottles -= 2;
            healthPot += 2;
        }
    }
    public void CraftProjectile()
    {
        if (mushrooms >= 4 && arcanaOrb >= 1)
        {
            mushrooms -= 4;
            arcanaOrb--;
            projectileSkill += 4;
        }
    }
    public void CraftBuffPot()
    {
        if (arcanaOrb >= 3 && emptyBottles >= 1)
        {
            emptyBottles--;
            arcanaOrb -= 3;
            skillBuffPot++;
        }
    }
    public void OpenLootBox()
    {
        if(treasureChests > 0)
        {
            treasureChests--;
            Instantiate(goldParticles, new Vector3(0, 0, 0), Quaternion.identity);
            int itemRoll = Random.Range(1, 101);
            if (itemRoll <= 50)
            {
                FindObjectOfType<GoldManager>().gold += Random.Range(25, 51) * FindObjectOfType<PlayerController>().playerLevel;
            }
            else if (itemRoll >= 51 && itemRoll < 75)
            {
                FindObjectOfType<GoldManager>().gold += Random.Range(200, 301) * FindObjectOfType<PlayerController>().playerLevel;
            }
            else if (itemRoll >= 75 && itemRoll < 90)
            {
                FindObjectOfType<GoldManager>().gold += Random.Range(500, 751) * FindObjectOfType<PlayerController>().playerLevel;
            }
            else if (itemRoll >= 90)
            {
                FindObjectOfType<GoldManager>().gold += Random.Range(1000, 5001) * FindObjectOfType<PlayerController>().playerLevel;
            }
            int gemRoll = Random.Range(0, 5);
            if (gemRoll == 1)
            {
                ruby++;
            }
            if (gemRoll == 2)
            {
                sapphire++;
            }
            if (gemRoll == 3)
            {
                topaz++;
            }
            if (gemRoll == 4)
            {
                blackDiamond++;
            }
        }
    }
    public void AddToClicks()
    {
        if (treasureChests > 0)
        {
            treasureChestClicks++;
        }
    }
}