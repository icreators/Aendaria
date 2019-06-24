using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ic
{ 
    public class Item : ScriptableObject
    {
        public new string name = "Item";
        public string description = "Your description here";

        public int weight = 0;
        private int amount = 1;
        private int level = 1;

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

        public virtual string GetStats()
        {
            string text = "Statystyki przedmiotu";
            return text;
        }

        public void RemoveFromInventory(int count = 1)
        {
            Inventory.instance.RemoveItem(this, count);
        }

        public void AddItem(int count = 1)
        {
            amount += count;
        }

        public int GetAmount()
        {
            return amount;
        }
    }

    public enum Type { Broń, Pancerz, Eliksiry, Jedzenie, Materiały, Inne}
}