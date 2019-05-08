using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    #region Singleton
    public static SkillsManager instance;

    private void Awake()
    {
        if (instance != null) Debug.LogWarning("More than one instance of SkillsManager found!");

        instance = this;
    }
    #endregion

    public void add(string name,string info="",int pkt=1)
    {
        this.GetComponent<SkillsOne>().add(name,info,pkt);
    }
    public bool IsActive(string name)
    {
        return this.GetComponent<SkillsOne>().CheckActive(name);
    }
    public bool NewActive(string name,int pkt)
    {
        return this.GetComponent<SkillsUI>().NewActive(name,pkt);
    }
    public bool IsVisible()
    {
        return this.GetComponent<SkillsUI>().isVisible;
    }
    public void AddExp(int type,float exp)
    {
        this.GetComponent<SkillsLevel>().addProcess(type,exp);
    }
}
