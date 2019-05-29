using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHand : MonoBehaviour
{
    private SkillsWindow win=new SkillsWindow();
    bool visible=false;

    MapUI map;

    void Start()
    {
        map=GameObject.Find("MapInt").GetComponent<MapUI>();
    }

    float backupx=0.5f;
    float backupy=0.5f;

    void Update()
    {
        Vector2 pos=new Vector2();

        if(Input.GetMouseButtonDown(0))
        {
            backupx=Input.mousePosition.x;
            backupy=Input.mousePosition.y;

            visible=true;
        }
        if(Input.GetMouseButtonUp(0))
            visible=false;

        if(visible)
            pos=Input.mousePosition;
        else
            pos=new Vector2(-1280,-720);

        //if(pos.x+300>1280)
        //    pos-=win.Locate(new Vector2(300,0));

        this.GetComponent<RectTransform>().sizeDelta=new Vector2(50,50);
        this.GetComponent<RectTransform>().position=pos;

        if(visible)
        {
            float ax=(backupx-Input.mousePosition.x)/1000.0f;
            float ay=(backupy-Input.mousePosition.y)/1000.0f;

            backupx=Input.mousePosition.x;
            backupy=Input.mousePosition.y;

            map.move(ax,ay);
        }

    }
}
