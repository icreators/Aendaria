using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public string title="";
    public string id_quest="";
    public List<QuestTarget> targets = new List<QuestTarget>();

    public bool questEnd=false;

    public bool FinishOnce()
    {
        if(questEnd)
            return false;
        foreach (QuestTarget target in targets) 
        {
            if(target.finished==false)
                return false;
        }
        questEnd=true;
        GameObject.Find("QuestsUI/Info").GetComponent<QuestInfo>().UIReset("Ukonczono: "+title);
        return true;
    }
    public int Procent()
    {
        int actual=0;
        foreach (QuestTarget target in targets) 
        {
            if(target.finished)
                actual++;
        }
        float actual2=(1.0f*actual/targets.Count)*100;
        return (int)actual2;
    }
    public void AddTarget(string desc,string id_target,int max)
    {
        QuestTarget target=new QuestTarget();
        target.set(desc,id_target,max);
        targets.Add(target);
    }
    public void AddProgress(string id_target,int add)
    {
        foreach (QuestTarget target in targets) 
        {
            if(target.id_target==id_target)
                target.AddProcess(add);
        }
    }
    public int FindTarget(string id_target)
    {
        int actual=0;
        foreach (QuestTarget target in targets) 
        {
            if(target.id_target==id_target)
                return actual;
            actual++;
        }
        return -1;
    }
}
