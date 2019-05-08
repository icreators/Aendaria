using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ic;

[CreateAssetMenu(fileName = "New ItemForCraft", menuName = "Inventory/Substance")]
public class Substance : Item
{
    public int craftingID = 1;

    public override void Use()
    {
        base.Use();
        //EquipmentManager.instance.Equip(this);
        //RemoveFromInventory();
    }
}