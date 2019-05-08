using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBar : MonoBehaviour
{
    public int type;

    DialogueWindow win=new DialogueWindow();
    private DialogueManager manager;

    float timer=0;

    float height=0;
    bool visible=false;

    void Start()
    {
        manager=GameObject.Find("Dial").GetComponent<DialogueManager>();
    }

    public void reset()
    {
        timer=0;
        height=0;
        visible=false;
    }

    void Update()
    {
        manager=GameObject.Find("Dial").GetComponent<DialogueManager>();

        visible=manager.IsVisible();
        if(visible)
            timer+=Time.deltaTime;
        else 
            timer-=Time.deltaTime;
        timer=win.cut(timer,1,0);


        height=timer*100;
        if(type==1)
            height=720-height;
        else
            height=height;
    
        this.GetComponent<RectTransform>().sizeDelta=win.Locate(new Vector2(1280,100));
        this.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2(0,height));
    }
}
