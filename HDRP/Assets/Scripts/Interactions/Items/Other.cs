using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ic;

[CreateAssetMenu(fileName = "New OtherItem", menuName = "Inventory/OtherItem")]
public class Other : Item
{

    public override void Use()
    {
        base.Use();
        //EquipmentManager.instance.Equip(this);
        //RemoveFromInventory();
    }
}