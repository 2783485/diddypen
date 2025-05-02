using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeManager : MonoBehaviour
{
    public enum GemType { None, Ruby, Sapphire, Topaz, BlackDiamond }
    public enum BladeType { None, BlackBlade, ArcaneMushroom }
    public GemType selectedGem = GemType.None;
    public BladeType selectedBlade = BladeType.None;
    private PlayerController player;
    private InventoryManager inv;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        inv = FindObjectOfType<InventoryManager>();
    }

    void Update()
    {

    }

    public void SetGemSelection(int index)
    {
        selectedGem = (GemType)index;
    }

    public void SetBladeSelection(int index)
    {
        selectedBlade = (BladeType)index;
    }

    public void CombineItems()
    {
        if (selectedBlade == BladeType.BlackBlade)
        {
            switch (selectedGem)
            {
                case GemType.Ruby:
                    if (inv.ruby > 0)
                    {
                        inv.ruby--;
                        player.rubiesInBlade++;
                        player.isFiery = true;
                    }
                    break;
                case GemType.Sapphire:
                    if (inv.sapphire > 0)
                    {
                        inv.sapphire--;
                        player.saphInBlade++;
                        player.isFiery = true;
                    }
                    break;
                case GemType.Topaz:
                    if (inv.topaz > 0)
                    {
                        inv.topaz--;
                        player.topazInBlade++;
                        player.isShocking = true;
                    }
                    break;
                case GemType.BlackDiamond:
                    if (inv.blackDiamond > 0)
                    {
                        inv.blackDiamond--;
                        player.bdInBlade++;
                        player.isCursed = true;
                    }
                    break;
            }
        }
        else if (selectedBlade == BladeType.ArcaneMushroom)
        {
            switch (selectedGem)
            {
                case GemType.Ruby:
                    if (inv.ruby > 0)
                    {
                        inv.ruby--;
                        player.rubiesInProj++;
                        player.isFieryProj = true;
                    }
                    break;
                case GemType.Sapphire:
                    if (inv.sapphire > 0)
                    {
                        inv.sapphire--;
                        player.saphInProj++;
                        player.isColdProj = true;
                    }
                    break;
                case GemType.Topaz:
                    if (inv.topaz > 0)
                    {
                        inv.topaz--;
                        player.topazInProj++;
                        player.isLightningProj = true;
                    }
                    break;
                case GemType.BlackDiamond:
                    if (inv.blackDiamond > 0)
                    {
                        inv.blackDiamond--;
                        player.bdInProj++;
                        player.isCursedProj = true;
                    }
                    break;
            }
        }
    }
}