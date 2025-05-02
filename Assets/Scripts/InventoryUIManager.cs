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
    public GameObject treasureChestText;
    public GameObject rubyText;
    public GameObject saphText;
    public GameObject topazText;
    public GameObject cdText;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<UIManager>().HideInventoryPanel();
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
        treasureChestText.GetComponent<TextMeshProUGUI>().text = FindObjectOfType<InventoryManager>().treasureChests.ToString() + "x Treasure Chests";
        rubyText.GetComponent<TextMeshProUGUI>().text = FindObjectOfType<InventoryManager>().ruby.ToString() + "x Rubies";
        saphText.GetComponent<TextMeshProUGUI>().text = FindObjectOfType<InventoryManager>().treasureChests.ToString() + "x Sapphires";
        topazText.GetComponent<TextMeshProUGUI>().text = FindObjectOfType<InventoryManager>().treasureChests.ToString() + "x Topaz";
        cdText.GetComponent<TextMeshProUGUI>().text = FindObjectOfType<InventoryManager>().treasureChests.ToString() + "x Cursed Diamonds";
    }
}
