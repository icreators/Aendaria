using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ic;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item
{
    public PotionType potionType;

    public int potionStrength = 20;
    public int effectTime = 120;

    public override void Use()
    {
        base.Use();

        ModificatorsManager.instance.ChangeModificator("health", potionStrength, 0, false, false);

        RemoveFromInventory();
    }
}

public enum PotionType { Health, Strength }