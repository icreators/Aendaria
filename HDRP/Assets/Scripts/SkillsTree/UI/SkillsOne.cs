using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsOne : MonoBehaviour
{
    public List<SkillsOne> skill= new List<SkillsOne>();
    public string name;
    public string info;
    public int height=0;
    public bool activated=false;
    public string fullName="";
    public int pkt=1;

    public int id(string actname)
    {
        for(int i=0;i<skill.Count;i++)
            if(skill[i].name==actname)
                return i;
        return -1;
    }
    public SkillsOne get(string actname)
    {
        string act="";
        for(int i=0;i<actname.Length;i++)
        {
            if(actname[i]!='/')
                act+=actname[i];
            
            if(actname[i]=='/'||(actname.Length-1==i))
            {
                int actid=id(act);
                if(actid!=-1)
                    return skill[actid].get(actname.Substring(i+1,actname.Length-(i+1)));
            }
        }

        return this;
    }
    /* public int GetTheLongest()
    {        
        int loong=skill.Count;
        if(loong!=0)
            foreach (SkillsOne act in skill) 
            {
                int actlong=act.GetTheLongest();
                if(actlong>loong)
                    loong=actlong;
            }
        return loong;
    }*/
    public int GetTheLongest()
    {        
        int actLong=0;
        foreach (SkillsOne act in skill) 
        {
            if(act.skill.Count>0)
                actLong+=act.GetTheLongest();
            else
                actLong+=1;
        }
        return actLong;
    }
    public void add(string actname, string info,int pkt=1,string actFullName="!")
    {
        if(actFullName=="!")
            actFullName=actname;
        
        string act="";
        for(int i=0;i<actname.Length;i++)
        {
            if(actname[i]=='/')
            {
                int actid=id(act);
                if(actid!=-1)
                {
                    skill[actid].add(actname.Substring(i+1,actname.Length-(i+1)),info,pkt,actFullName);
                    return;
                }
            }
            act+=actname[i];
        }
        SkillsOne newskill=new SkillsOne();
        newskill.name=actname;
        newskill.info=info;
        newskill.height=height+1;
        newskill.fullName=actFullName;
        newskill.pkt=pkt;
        if(newskill.info=="category")
            newskill.activated=true;
        skill.Add(newskill);
    }
    public bool CheckActive(string loc)
    {
        return get(loc).activated;
    }
    public string Last(string path="!")
    {
        string actname;
        if(path=="!")
            actname=fullName;
        else
            actname=path;
        string act="";
        string newact="";
        for(int i=0;i<actname.Length;i++)
        {
            if(actname[i]=='/')
            {
                if(newact!="")
                    newact+="/";
                newact+=act;
                act="";
            }
            else
                act+=actname[i];
        }
        return newact;
    }
    public bool NewActive(string actname)
    {
        string act="";
        for(int i=0;i<actname.Length;i++)
        {
            if(actname[i]!='/')
                act+=actname[i];

            if(actname[i]=='/'||(actname.Length-1==i))
            {
                int actid=id(act);
                if(actid!=-1)
                {
                    if(skill[actid].activated||actname.Length-1==i)
                        return skill[actid].NewActive(actname.Substring(i+1,actname.Length-(i+1)));   
                    else
                        return false;
                }
            }
        }
        if(activated==false)
        {
            activated=true;
            return true;
        }
        else
            return false;
    }

}
