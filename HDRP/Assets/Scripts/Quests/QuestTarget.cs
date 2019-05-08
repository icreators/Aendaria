using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTarget : MonoBehaviour
{
    public string description;
    public string id_target;

    public int actualProgress=0;
    public int maxProgress=0;

    public bool finished=false;

    public void set(string newDescription,string NewIdTarget,int newMaxProgress)
    {
        description=newDescription;
        maxProgress=newMaxProgress;
        id_target=NewIdTarget;
    }
    public void AddProcess(int HowMuch)
    {
        if(actualProgress<maxProgress)
        {
            actualProgress++;
            
            if(actualProgress>=maxProgress)
            {
                finished=true;
            }
            
            GameObject.Find("QuestsUI").GetComponent<QuestUI>().ProcessUpdate();
        }
        
    }
    public int procent()
    {
        float actual=(1.0f*actualProgress/maxProgress)*100.0f;
        return (int)actual;
    }
}
