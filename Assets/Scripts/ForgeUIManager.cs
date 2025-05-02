using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgeUIManager : MonoBehaviour
{
    public Sprite bladeSprite;
    public Sprite arcaneMushroomSprite;
    public Sprite rubySprite;
    public Sprite sapphireSprite;
    public Sprite topazSprite;
    public Sprite blackDiamondSprite;
    public Image item1;
    public Image item2;

    ForgeManager forge;

    void Start()
    {
        forge = FindObjectOfType<ForgeManager>();
        FindObjectOfType<UIManager>().HideForgePanel();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        switch (forge.selectedBlade)
        {
            case ForgeManager.BladeType.BlackBlade:
                item1.sprite = bladeSprite;
                break;
            case ForgeManager.BladeType.ArcaneMushroom:
                item1.sprite = arcaneMushroomSprite;
                break;
            default:
                item1.sprite = null;
                break;
        }

        switch (forge.selectedGem)
        {
            case ForgeManager.GemType.Ruby:
                item2.sprite = rubySprite;
                break;
            case ForgeManager.GemType.Sapphire:
                item2.sprite = sapphireSprite;
                break;
            case ForgeManager.GemType.Topaz:
                item2.sprite = topazSprite;
                break;
            case ForgeManager.GemType.BlackDiamond:
                item2.sprite = blackDiamondSprite;
                break;
            default:
                item2.sprite = null;
                break;
        }
    }
}
