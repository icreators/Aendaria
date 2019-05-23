using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;

    //List<InventorySlot> slots = new List<InventorySlot>(); 

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangeCallback += UpdateUI;

        inventoryUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (!InterfaceManager.instance.isAnyActiveInterface)
            {
                InterfaceManager.instance.isAnyActiveInterface = true;

                inventoryUI.SetActive(true);
                inventory.InventoryRefresh();
            }
            else
            {
                InterfaceManager.instance.isAnyActiveInterface = false;

                inventoryUI.SetActive(false);
            }
        }
    }

    

    public void DeleteSlots()
    {
        //slots = null;
    }

    void UpdateUI()
    {
        /*foreach (Transform child in itemsParent)
        {
            Debug.Log("Updating UI");

            child.GetComponent<>
        }*/
        InventorySlot[] slots;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        Debug.Log("Updating UI<numberOfSlots>" + slots.Length);

        for (int i=0; i<slots.Length; i++)
        {
            if (i < inventory.items.Count) slots[i].AddItem(inventory.items[i]);
            else slots[i].ClearSlot();
        }
    }
}