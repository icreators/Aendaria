using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ic;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    public static new EquipmentSlot equipSlot = EquipmentSlot.Weapon;

    public int weaponStrength;

    public override void Use()
    {
        base.Use();
    }
}