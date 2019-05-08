using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListTarget : MonoBehaviour
{
    private GameObject list;
    private GameObject sample;
    private QuestWindow win=new QuestWindow();
    private GameObject questTitle;

    private List<QuestTarget> targetBackup=new List<QuestTarget>();
    private int ActualScroll=0;
    private int MaxScroll=0;

    void Start()
    {
        sample=GameObject.Find("QuestsUI/Panel/TargetSample");
        list=GameObject.Find("QuestsUI/Panel/ListTarget");
        questTitle=GameObject.Find("QuestsUI/Panel/Description");

        questTitle.GetComponent<QuestListTargetSample>().set("Wybierz zadanie",101);
    }

    public void ReplaceUITarget(string questName="",int newProcent=0)
    {
        RemoveList();
        NewList(questName,newProcent);
    }
    public void changeTarget(List<QuestTarget> targets)
    {
        targetBackup=targets;
    }
    void NewList(string questName="",int newProcent=0)
    {
        MaxScroll=0;
        ActualScroll=0;
        if(questName!="")
        {
            questTitle.GetComponent<QuestListTargetSample>().set(questName,newProcent);
        }

        int size_y=1;
        foreach (QuestTarget target in targetBackup) 
        {
            GameObject element=Instantiate(sample);
            element.active=true;
            element.transform.SetParent(list.transform);
            element.GetComponent<QuestListTargetSample>().set(target.description,target.procent());
            element.GetComponent<RectTransform>().sizeDelta=win.Locate(new Vector2(666,100));
            element.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2(486,(120*-size_y)+720-30));
            MaxScroll+=120;
            size_y++;
        }
    }
    public void RemoveList()
    {
        foreach (Transform child in list.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void ChangeTitlePosition()
    {
        questTitle.GetComponent<RectTransform>().sizeDelta=win.Locate(new Vector2(666,130));
        questTitle.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2(486,720));
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
        if(ActualScroll>MaxScroll-720+120+30||ActualScroll<0||MaxScroll<720)
        {
            for(int i=0;i<10;i++)
                NormalizeScroll();
        
        }
    }
}
