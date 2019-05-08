using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestListTargetSample : MonoBehaviour
{
    public string description;
    public int progress;
    private QuestWindow win=new QuestWindow();

    public void set(string newDescription,int newProgress)
    {
        description=newDescription;
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=description;

        progress=newProgress;
        if(newProgress>100)
            progress=0;

        float procent=(progress/100.0f);
        this.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta=win.LocateVector3(new Vector2(666*procent,20));
        
        string txt="";
        if(newProgress<100&&newProgress>=0)
            txt=progress+"%";
        else if(newProgress==100)
            txt="ukonczone";
        this.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=txt;

    }
}
