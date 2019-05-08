using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotSpawn : MonoBehaviour
{
    [SerializeField] GameObject itemSlotP;
    [SerializeField] GameObject itemDropP;
    [SerializeField] TextMeshProUGUI itemNameP;
    [SerializeField] RectTransform itemsParentP;
    
    public void Start()
    {
        Inventory.instance.itemSlot = itemSlotP;
        Inventory.instance.itemDrop = itemDropP;
        Inventory.instance.itemsParent = itemsParentP;
        Inventory.instance.typeText = itemNameP;
    }
}
