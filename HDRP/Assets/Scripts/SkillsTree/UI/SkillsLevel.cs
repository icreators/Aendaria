using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsLevel : MonoBehaviour
{
    public List<SkillsLevelSegment> levels=new List<SkillsLevelSegment>();

    public void addProcess(int type,float exp)
    {
        if(type>=3)
            return;
        levels[type].process+=exp;
    }
    public void set(int type,int level,int points,int process)
    {
        if(type>=3)
            return;
        levels[type].level=level;
        levels[type].points=points;
        levels[type].process=process;
    }

    void Start()
    {
        levels.Add(new SkillsLevelSegment("Zręczność"));
        levels.Add(new SkillsLevelSegment("Walka"));
        levels.Add(new SkillsLevelSegment("Charyzma"));
    }

    void Update()
    {
        foreach(SkillsLevelSegment segment in levels)
        {
            segment.maxProgress=(segment.level+1)*100;
            if(segment.process>=segment.maxProgress)
            {
                segment.process-=segment.maxProgress;
                segment.level++;
                segment.points+=3;
                Debug.Log("Level Up!");
            }
        }
    }
}
public class SkillsLevelSegment
{
    public SkillsLevelSegment(string actName)
    {
        name=actName;
    }

    public string name;
    public int level=0,points=3;
    public float process=0.0f,maxProgress=0.0f;

    public int procent()
    {
        float proc=(1.0f*process/maxProgress)*100;
        return (int)proc;
    }
}