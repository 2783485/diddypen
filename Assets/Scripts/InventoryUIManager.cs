using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject arcaneOrbText;
    public GameObject mushroomsText;
    public GameObject emptyBottlesText;
    public GameObject healPotText;
    public GameObject buffPotText;
    public GameObject magicProjectileText;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        arcaneOrbText.GetComponent<TextMeshProUGUI>().text = FindObjectOfType<InventoryManager>().arcanaOrb.ToString() + "x Arcane Orbs";
        mushroomsText.GetComponent<TextMeshProUGUI>().text = FindObjectOfType<InventoryManager>().mushrooms.ToString() + "x Mushrooms";
        emptyBottlesText.GetComponent<TextMeshProUGUI>().text = FindObjectOfType<InventoryManager>().emptyBottles.ToString() + "x Empty Potion Bottles";
        healPotText.GetComponent<TextMeshProUGUI>().text = FindObjectOfType<InventoryManager>().healthPot.ToString() + "x Healing Potions";
        buffPotText.GetComponent<TextMeshProUGUI>().text = FindObjectOfType<InventoryManager>().skillBuffPot.ToString() + "x Buffing Potions";
        magicProjectileText.GetComponent<TextMeshProUGUI>().text = FindObjectOfType<InventoryManager>().projectileSkill.ToString() + "x Arcane Mushrooms";
    }
}
