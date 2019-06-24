using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ic;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null) Debug.LogWarning("More than one instance of Inventory found!");

        instance = this;
    }
    #endregion

    public List<Item> items = new List<Item>();     // Item List
    private static float baseWeightLimit = 100;
    private float weightLimit = baseWeightLimit;
    private float currentWeight = 0;

    public delegate void OnItemChange();
    public OnItemChange onItemChangeCallback;

    public RectTransform itemsParent;
    public GameObject itemSlot;
    public GameObject itemDrop;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI weightText;

    public int actualType = 1;
    private string actualTypeName = "Equipment";

    // ----------------- Functions ----------------- //

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Itemy w eq: ");

            foreach (Item item in items) Debug.Log(item.name);

            Debug.Log(EqItemCheck("Super Tarcza"));
        }
    }

    public int EqItemCheck(string itemName, int count = 0)
    {
        foreach (Item item in items) if (item.name == itemName)
            {
                if (count != 0)
                {
                    item.AddItem(count);
                }

                return item.GetAmount();
            }
        return 0;
    }

    public bool AddItem(Item item, int count = 1)
    {
        if (!item.isDefaultImte)
        {
            if (EqItemCheck(item.name, count) > 0) Debug.Log("Dodawanie istniejacego itemu " + item.name);
            else
            {
                items.Add(item);

                //if (onItemChangeCallback != null) onItemChangeCallback.Invoke();

                InventoryRefresh();

                Debug.Log("Dodanie nowego itemu o nazwie " + item.name + " do wyposażenia");
            }
        }
        return true;
    }

    public void RemoveItem(Item item, int count = 1 ,bool drop = false)
    {
        if (drop) DropItem(item);

        if (item.GetAmount() > 1) item.AddItem(-count);
        else items.Remove(item);

        Debug.Log("Usuwanie itemu o nazwie " + item.name);

        InventoryRefresh();
    }

    private void DropItem(Item item)
    {
        GameObject dropItem = Instantiate(item.prefab);

        dropItem.transform.position = itemDrop.transform.position;
    }

    public void InventoryRefresh()
    {
        foreach (Transform slot in itemsParent.transform)
        {
            DeleteSlots(slot);
        }
        DrawItems();

        typeText.text = actualTypeName;
    }

    public void DrawItems()
    {
        float ItemsWeight = 0;

        foreach (Item item in items)
        {
            int temp = (Convert.ToInt32(item.type)) + 1;

            //Debug.Log(temp + " == " + actualType);

            if (temp == actualType)
            {
                actualTypeName = item.type.ToString();

                InventorySlot newItemSlot = Instantiate(itemSlot).GetComponent<InventorySlot>();

                newItemSlot.transform.parent = itemsParent.transform;

                newItemSlot.AddItem(item);

                ItemsWeight += (item.weight * item.GetAmount());
            }
        }

        currentWeight = ItemsWeight;

        weightText.text = "Bagaż " + currentWeight.ToString() + "/" + weightLimit;
    }

    public void DeleteSlots(Transform slot)
    {
        Destroy(slot.gameObject);
    }





    /*public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangeCallback != null) onItemChangeCallback.Invoke();

        Debug.Log("Usuwanie nowego itemu o nazwie " + item.name + " do wyposażenia zostało wykonane pomyślnie");
    }*/



    /*public void TypeClick(int type)
    {
        //Debug.Log("-TypeClick-Liczba itemow przed usunieciem: " + items.Count);
       
        
            
                ChangeSlots(itemsParent, hiddenSlots); Debug.Log("Ukrywanie wszytkich itemow");
                DrawSlots(hiddenSlots, itemsParent, type); Debug.Log("Pokazywanie itemow");
                //DrawSlots();
                
        
    }*/

    /*private void ChangeSlots(RectTransform from, RectTransform to, int type = 1)
    {
        int itemsInChildren = from.GetChild(0).GetComponentsInChildren<Transform>().Length;

        int nOfChilds = (from.GetComponentsInChildren<Transform>().Length / itemsInChildren);



        Debug.Log("Przycisk " + type);

        for(int x=0; x<nOfChilds; x++)
        {
            int itemType = Convert.ToInt32(from.GetChild(0).transform.GetComponent<InventorySlot>().item.type);

            
            
                Debug.Log("typ itemu " + itemType);

                Debug.Log(itemType);

                Debug.Log("Przenoszenie itemka");
            
             
                from.GetChild(0).transform.parent = to.transform;
             
        }
    }*/

    /*private void DrawSlots(RectTransform from, RectTransform to, int type = 1)
    {
        int child = 0;

        int itemsInChildren = from.GetChild(0).GetComponentsInChildren<Transform>().Length;

        int nOfChilds = (from.GetComponentsInChildren<Transform>().Length / itemsInChildren);



        Debug.Log("Przycisk " + type);

        for (int x = 0; x < nOfChilds; x++)
        {

            int itemType = Convert.ToInt32(from.GetChild(child).transform.GetComponent<InventorySlot>().item.type);



            Debug.Log("typ itemu " + itemType);

            Debug.Log(itemType + 0);

            int temp = itemType + 1;

            Debug.Log("Whether" + temp + " = " + type + " nrChilda: " + child);
            if (itemType + 1 == type)
            {
                Debug.Log("Must be true(if not kill yourself): " + temp + " = " + type + " nrChilda: " + child);

                from.GetChild(child).transform.parent = to.transform;
            }
            else child++;
        }
            

            
    }*/
    


    /*private void Del()
    {
        foreach (Transform child in itemsParent)
        {
            Debug.Log("wys");

            child.transform.parent = hiddenSlots.transform;

        }
        if (itemsParent.GetComponentsInChildren<Transform>().Length > 0) { Debug.Log("Liczba dzieci:" + itemsParent.GetComponentsInChildren<Transform>().Length);Del(); }
    }*/
    /*public void DrawSlots()
    {
        Debug.Log("rysowanie slotow");

        Debug.Log("Liczba itemow po usunieciu: " + items.Count);
        for (int i=0; i<items.Count; i++)
        {
            Debug.Log("iteracja");
            var newItemSlot = Instantiate(itemSlot);
            newItemSlot.transform.parent = itemsParent.transform;

            
        }

            if (onItemChangeCallback != null) onItemChangeCallback.Invoke();
   

        
    }*/
}