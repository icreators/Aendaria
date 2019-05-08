using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillsElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string name;
    public string info;
    public string fullName;
    public int pkt;

    private float size_x = 64;
    private float size_y = 64;

    private SkillsWindow win=new SkillsWindow();

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("SkillsTree/Panel/Info").GetComponent<SkillsInfo>().SetText(name+"("+pkt+"pkt)"+"\n"+info);
        GameObject.Find("SkillsTree/Panel/Info").GetComponent<SkillsInfo>().visible=true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject.Find("SkillsTree/Panel/Info").GetComponent<SkillsInfo>().visible=false;
    }

    public void SetColor(Color kolor)
    {
        this.GetComponent<Image>().color=kolor;
    }
    public void SetImage(string src)
    {
        Sprite picture;
        if(picture=Resources.Load<Sprite>("Skills/Icon/"+src))
            this.transform.GetChild(0).GetComponent<Image>().sprite=picture;
    }
    public void SetInfo(string actname,string actinfo,string actFullName,int actPkt)
    {
        name=actname;
        info=actinfo;
        fullName=actFullName;
        pkt=actPkt;
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }
    public void Pos(float x,float y)
    {
        this.GetComponent<RectTransform>().sizeDelta=win.Locate(new Vector2(size_x,size_y));
        this.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2((x*size_x)+(size_x/2),(y*size_y)+(size_y/2)));
    }
    public void OnClick()
    {
        Debug.Log(fullName);
        GameObject.Find("SkillsTree").GetComponent<SkillsUI>().NewActive(fullName,pkt);
    }
}
