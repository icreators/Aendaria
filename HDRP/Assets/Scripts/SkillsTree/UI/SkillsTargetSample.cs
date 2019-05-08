using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillsTargetSample : MonoBehaviour
{
    public string description;
    public int progress;
    private SkillsWindow win=new SkillsWindow();

    public void set(string newDescription,int newProgress)
    {
        Pos();
        description=newDescription;
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=description;

        progress=newProgress;
        if(newProgress>100)
            progress=0;

        float procent=(progress/100.0f);
        this.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta=win.LocateVector3(new Vector2(450*procent,20));
        
        string txt="";
        if(newProgress<=100&&newProgress>=0)
            txt=progress+"%";
        this.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=txt;

    }
    private void Pos()
    {
        this.GetComponent<RectTransform>().sizeDelta=win.Locate(new Vector2(500,100));
        this.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2(1280-500,720-100));
    }
}
