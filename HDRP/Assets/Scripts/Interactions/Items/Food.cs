        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ic;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class Food : Item
{
    public int healthAdd;

    public override void Use()
    {
        base.Use();
        //EquipmentManager.instance.Equip(this);
        //RemoveFromInventory();
    }
}