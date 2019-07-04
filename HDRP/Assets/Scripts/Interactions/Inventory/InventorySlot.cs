using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ic;


public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    public Item item;

    [SerializeField] TextMeshProUGUI itemName; 
    [SerializeField] TextMeshProUGUI itemDescription;
    [SerializeField] TextMeshProUGUI itemStats;
    [SerializeField] TextMeshProUGUI itemAmount;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AddItem (Item newItem)
    {
        Debug.Log("Adding new item <name>" + newItem.name);

        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        removeButton.interactable = true;
        itemName.text = item.name;
        itemDescription.text = item.description;
        itemStats.text = item.GetStats();

        itemAmount.text = item.GetAmount().ToString();

        itemDescription.gameObject.SetActive(false);
        itemStats.gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;
    }

    public void OnRemoveButton(bool drop)
    {
        Inventory.instance.RemoveItem(item, 1, drop);
    }

    public void UseItem()
    {
        if (item != null) item.Use();
    }

    public void MouseEnter()
    {
        itemStats.gameObject.SetActive(false);
        itemDescription.gameObject.SetActive(true);

        animator.SetTrigger("Dup");
    }

    public void MouseExit()
    {
        itemDescription.gameObject.SetActive(false);
        itemStats.gameObject.SetActive(true);

        animator.SetTrigger("Ddown");
    }

}
