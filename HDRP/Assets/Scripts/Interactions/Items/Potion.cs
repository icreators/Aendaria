using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ic;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item
{
    public PotionType potionType;

    public int effectTime = 120;

    public override void Use()
    {
        base.Use();
        //EquipmentManager.instance.Equip(this);
        //RemoveFromInventory();
    }
}

public enum PotionType { Health, Strength }