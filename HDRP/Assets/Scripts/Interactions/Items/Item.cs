using UnityEngine;
namespace ic { 
//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public new string name = "Item";
    public string description = "Your description here";

    public GameObject prefab;

    public Type type;

    public Sprite icon = null;
    public bool isDefaultImte = false;

    public int value;

    public virtual void Use()
    {
        // something in using item;

        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveItem(this);
    }
}

public enum Type { Broń, Pancerz, Eliksiry, Jedzenie, Materiały, Inne}
}