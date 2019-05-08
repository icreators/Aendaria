using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public bool UIVisible=false;

    private RectTransform panelTransform;
    private GameObject panel;
    private QuestWindow win= new QuestWindow();
    
    private QuestList list;
    private QuestListTarget targetlist;
    private GameObject questTitle;
    private GameObject background;

    private Vector2 resolution;
    private string actualQuest="";

    private int power=0;
    private bool off=false;


    void Start()
    {
        panelTransform=GameObject.Find("QuestsUI/Panel").GetComponent<RectTransform>();
        panel=GameObject.Find("QuestsUI/Panel");
        questTitle=GameObject.Find("QuestsUI/Panel/Description");
        background=GameObject.Find("QuestsUI/Background");

        list=this.GetComponent<QuestList>();
        targetlist=this.GetComponent<QuestListTarget>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Quests"))
        {
            QuestOnOff();
        }
        if(UIVisible)
        {
            panelTransform.sizeDelta=win.Locate(new Vector2(1024,720));
            panelTransform.position=win.LocateVector3(new Vector2(128,0));
            ResolutionUpdate();
            Scroll();
        }
        if(!off)
        {
            QuestOnOff();
            off=true;
        }
    }

    void QuestOnOff()
    {
        if(UIVisible==true)
        {
            RemoveList();
            UIVisible=false;
        }
        else
        {
            UIUpdate();
            UIVisible=true;
        }
        background.active = UIVisible;
        panel.GetComponent<Image>().enabled = UIVisible;
    }

    public void TargetUI(string id_quest)
    {
        int id=list.Find(id_quest);
        if(id!=-1)
        {
            actualQuest=id_quest;
            string newName=list.quests[id].title;
            int newProcent=list.quests[id].Procent();
            targetlist.changeTarget(list.quests[id].targets);
            targetlist.ReplaceUITarget(newName,newProcent);
        }
    }
    public void ProcessUpdate()
    {
        if(UIVisible)
            UIUpdate();
    }
    void ResolutionUpdate()
    {
        Vector2 newResolution=new Vector2(Screen.width,Screen.height);
        if(newResolution!=resolution)
        {
            resolution=newResolution;
            UIUpdate();
        }
    }
    void UIUpdate()
    {
        power=0;
        questTitle.active=true;
        targetlist.ChangeTitlePosition();
        //targetlist.ReplaceUITarget();
        TargetUI(actualQuest);
        list.ReplaceUI();
    }
    void RemoveList()
    {
        questTitle.active=false;
        targetlist.RemoveList();
        list.RemoveList();
    }
    void NormalizeScroll()
    {
        if(power>0)
        power--;
        else
        if(power<0)
        power++;
    } 
    void Scroll()
    {
        Vector2 drop=win.Locate(new Vector2(358+128,0));
        Vector3 mouse = Input.mousePosition;

        float scroll=Input.GetAxis("Mouse ScrollWheel");

        NormalizeScroll();
        if(scroll != 0f)
        {
            if(scroll<0f)
                power=-15;
            else
                power=15;

        }
        list.ScrollAuto();
        targetlist.ScrollAuto();

        if(mouse.x>drop.x)
            targetlist.scroll(power);
        else
            list.scroll(power);
    }

}
