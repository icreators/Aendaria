using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EquipmentManager : MonoBehaviour
{
    #region Singlenton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    Equipment[] currentEquipment;

    public delegate void OnEquipmentChange(Equipment newItem, Equipment olldItem);
    public OnEquipmentChange onEquipmentChange;

    Inventory inventory;

    [SerializeField] RectTransform Equipment;

    [SerializeField] List<Button> eqButtons = new List<Button>();

    [SerializeField] Sprite slotBackground;

    private void Start ()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        currentEquipment = new Equipment[numSlots];

        foreach (Transform n in Equipment)
        {
            eqButtons.Add(n.GetComponent<Button>());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) UnequipAll();
    }

    public void Equip (Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Inventory.instance.RemoveItem(newItem);

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];

            inventory.AddItem(oldItem);
        }

        Button button = eqButtons.ElementAt(Convert.ToInt32(newItem.equipSlot));

        button.GetComponentInChildren<Image>().sprite = newItem.icon;

        // dodac do wyposazenia poprzedni item jesli istnieje

        if (onEquipmentChange != null) onEquipmentChange.Invoke(newItem, oldItem);

        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip (int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChange != null) onEquipmentChange.Invoke(null, oldItem);

            // Clear Slot

            Button button = eqButtons.ElementAt(Convert.ToInt32(slotIndex));

            button.image.sprite = slotBackground;
        }
    }

    public void UnequipAll ()
    {
        for (int i=0; i<currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }
}