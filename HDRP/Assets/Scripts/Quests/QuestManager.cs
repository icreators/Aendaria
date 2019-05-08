using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private QuestList list;

    void Start()
    {
        list=this.GetComponent<QuestList>();
    }

    public bool QuestIsExist(string id_quest)
    {
        if(list.Find(id_quest)!=-1)
            return true;
        return false;
    }
    public void AddQuest(string name,string id_quest)
    {
        if(list.Find(id_quest)==-1)
            list.AddQuest(name,id_quest);
    }
    public void AddTarget(string id_quest,string name,string id_target,int max)
    {
        if(list.FindTarget(id_quest,id_target)==-1)
            list.AddTarget(id_quest,name,id_target,max);
    }
    public void AddProgress(string id_quest,string id_target,int add)
    {
        list.AddProgress(id_quest,id_target,add);
    }
    public bool IsOpen()
    {
        return this.GetComponent<QuestUI>().UIVisible;
    }
    public bool Reward(string id_quest)
    {
        return list.FinishOnce(id_quest);
    }
}
