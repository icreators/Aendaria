using UnityEngine;
using UnityEngine.UI;
using ic;

public class PlayerStats : CharacterStats
{
    [SerializeField] Slider armorSlide;

    void Start()
    {
        EquipmentManager.instance.onEquipmentChange += OnEquipmentChanged;
    }

    void OnEquipmentChanged (Item newItem, Item oldItem)
    {
        if (newItem != null)
        {
            //armor.AddModifier(newItem.armorModifier);
            
            armorSlide.value = armor.GetValue();
        }

        if (oldItem != null)
        {
            //armor.RemoveModifier(oldItem.armorModifier);
        }
        
    }
}
