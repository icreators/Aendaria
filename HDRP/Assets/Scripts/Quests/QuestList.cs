using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
{
    public List<Quests> quests = new List<Quests>();

    private GameObject list; 
    private GameObject sample;

    private QuestWindow win= new QuestWindow();
    private int ActualScroll=0;
    private int MaxScroll=0;

    void Start()
    {
        list = GameObject.Find("QuestsUI/Panel/List");
        sample = GameObject.Find("QuestsUI/Panel/ListSample");
    }

    public void AddQuest(string title,string id_quest)
    {
        GameObject.Find("QuestsUI/Info").GetComponent<QuestInfo>().UIReset(title);
        Debug.Log("Rozpoczęto nowy quest! "+title);
        Quests quest=new Quests();
        quest.title=title;
        quest.id_quest=id_quest;
        quests.Add(quest);
    }
    public void AddTarget(string id_quest,string name,string id_target,int max)
    {
        int id=Find(id_quest);
        if(id!=-1)
        {
            Debug.Log("Utworzono nowy cel! "+name);
            quests[id].AddTarget(name,id_target,max);
        }
    }
    public void AddProgress(string id_quest,string id_target,int add)
    {
        int id=Find(id_quest);
        if(id!=-1)
        {
            quests[id].AddProgress(id_target,add);
        }
    }

    public int Find(string id_quest)
    {
        int actual=0;
        foreach (Quests quest in quests) 
        {
            if(quest.id_quest==id_quest)
                return actual;
            actual++;
        }
        return -1;
    }
    public int FindTarget(string id_quest,string id_target)
    {
        int id=Find(id_quest);
        if(id!=-1)
        {
            return quests[id].FindTarget(id_target);
        }
        return -1;
    }

    public void ReplaceUI()
    {
        RemoveList();
        NewList();
    }

    void NewList()
    {
        ActualScroll=0;
        MaxScroll=0;

        int size_y=0;
        foreach (Quests quest in quests) 
        {
            if(!quest.questEnd)
            {
                GameObject element=Instantiate(sample);
                element.active=true;
                element.transform.SetParent(list.transform);
                element.GetComponent<QuestListElement>().set(quest.title,quest.id_quest);
                element.GetComponent<RectTransform>().sizeDelta=win.Locate(new Vector2(358,120));
                element.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2(128,(120*-size_y)+720));
                
                size_y++;
                MaxScroll+=120;
            }
        }
    }
    public void RemoveList()
    {
        foreach (Transform child in list.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public bool FinishOnce(string id_quest)
    {
        int id=Find(id_quest);
        if(id!=-1)
            return quests[id].FinishOnce();
        return false;
    }

    bool NormalizeScroll()
    {
        if(ActualScroll<0)
            scroll(1);
        if(ActualScroll>0) 
            scroll(-1); 
        if(ActualScroll==0)
        return true;
        return false;
    }
    public void scroll(int y)
    {
        ActualScroll+=y;
        list.GetComponent<RectTransform>().position+=win.LocateVector3(new Vector2(0,y));
    }
    public void ScrollAuto()
    {
        if(ActualScroll>MaxScroll-720||ActualScroll<0||MaxScroll<720)
        {
            for(int i=0;i<10;i++)
                NormalizeScroll();
        
        }
    }
}
