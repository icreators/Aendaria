using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestInfo : MonoBehaviour
{
    private QuestWindow win=new QuestWindow();
    private float timer=0;
    private string text="";

    void Update()
    {
        UIdown();
    }

    public void UIReset(string title)
    {
        text=title;
        timer=5;

        this.GetComponent<RectTransform>().sizeDelta=win.Locate(new Vector2(1000,100));
        this.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2(140,720));
        this.GetComponent<TextMeshProUGUI>().text=text;
        this.GetComponent<TextMeshProUGUI>().color=new Color(1,1,1,1);
    }
    void UIdown()
    {
        if(timer>0)
        {
            timer-=Time.deltaTime*2;
        }
        else
            this.GetComponent<TextMeshProUGUI>().color-=new Color(0,0,0,Time.deltaTime);
        this.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2(140,720+(timer*20)-(5*20)));

        if(this.GetComponent<TextMeshProUGUI>().color.a<=0)
            this.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2(140,720));
    }
}
