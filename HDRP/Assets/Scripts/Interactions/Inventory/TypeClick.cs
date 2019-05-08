using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ic;

public class TypeClick : MonoBehaviour
{
    public void ButtonOnclick(int arg)
    {
        foreach (Item item in Inventory.instance.items)
            if (Convert.ToInt32(item.type) + 1 == arg)
            {
                Debug.Log(Convert.ToInt32(item.type) + "+1 = " + arg);

                Inventory.instance.actualType = arg;

                Inventory.instance.InventoryRefresh();
            }
    }
}
