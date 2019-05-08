using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ic;

public class EQSlot : MonoBehaviour
{
    public Image icon;

    public Item item;

    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDescription;

    public void EquipItem (Item newItem)
    {
        Debug.Log("Equiping new item <name>" + newItem.name);

        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        //itemName.text = item.name;
        //itemDescription.text = item.description;
    }

    public void Unequip()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnRemoveButton(bool drop)
    {
        Inventory.instance.RemoveItem(item, drop);
    }

    public void UseItem()
    {
        if (item != null) item.Use();
    }
}
