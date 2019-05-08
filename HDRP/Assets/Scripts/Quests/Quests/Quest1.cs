using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ic;

public class Quest1 : MonoBehaviour
{
    private QuestManager questManager;
    private DialogueManager dialManager;

    public Item potion;
    public Item marchew;

    void Start()
    {
        questManager=GameObject.Find("QuestsUI").GetComponent<QuestManager>();
        dialManager=GameObject.Find("Dial").GetComponent<DialogueManager>();
    }

    void Update()
    {
        if(dialManager.Once("14"))
        {
            questManager.AddQuest("Skryba","Skryba");
            questManager.AddTarget("Skryba","Przynies marchew skrybie","marchew",1);
            questManager.AddTarget("Skryba","Zdaj mature z polskiego","polski",1);
        }
        if(questManager.QuestIsExist("Skryba"))
        {
            if(dialManager.Once("13"))
                questManager.AddProgress("Skryba","polski",1);
            if(Inventory.instance.EqItemCheck("Marchew"))
                questManager.AddProgress("Skryba","marchew",1);
        }

        if(questManager.Reward("Skryba"))
        {
            questManager.AddQuest("Odnies marchew","OddajSkrybie");
            questManager.AddTarget("OddajSkrybie","Oddaj marchew skrybie","marchew",1);
            this.GetComponent<CharracterConv>().ID="14";
        }

        if(questManager.QuestIsExist("OddajSkrybie"))
        {
            if(dialManager.ID()=="14")
                questManager.AddProgress("OddajSkrybie","marchew",1);
        }

        if(questManager.Reward("OddajSkrybie"))
        {
            Debug.Log("Quest ukończony");
            Inventory.instance.RemoveItem(marchew);
            Inventory.instance.AddItem(potion);
        }
    }
}
