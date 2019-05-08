using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillsInfo : MonoBehaviour
{
    private SkillsWindow win=new SkillsWindow();
    public bool visible=false;

    void Start()
    {
        visible=false;
    }
    void Update()
    {
        Vector2 pos=new Vector2();

        if(visible)
            pos=Input.mousePosition;
        else
            pos=new Vector2(-1280,-720);

        if(pos.x+300>1280)
            pos-=win.Locate(new Vector2(300,0));
        this.GetComponent<RectTransform>().sizeDelta=win.Locate(new Vector2(300,200));
        this.GetComponent<RectTransform>().position=pos;
    }
    public void SetText(string text)
    {
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=text;
    }
}
