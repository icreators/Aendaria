using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    private SkillsWindow win=new SkillsWindow();
    public bool isVisible=true;

    public Transform player;

    GameObject map;
    GameObject circle;
    GameObject panel;
    GameObject back;

    float offx=0.5f;
    float offy=0.5f;

    float zoom=0.5f;
    float centerx=0.0f;
    float centery=0.0f;

    public Vector2 pointPos=new Vector2();


    void Start()
    {
        map=GameObject.Find("MapInt/Panel/LMAP");
        circle=GameObject.Find("MapInt/Circle");
        panel=GameObject.Find("MapInt/Panel");
        back=GameObject.Find("MapInt/Circle/back");
    }

    bool start=false;

    void Update()
    {    
        if (Input.GetButtonDown("Map")||start==false)
        {
            isVisible=!isVisible;
            start=true;
        }
        if(isVisible)
        {
            panel.SetActive(true); 
            //Debug.Log(player.position);
            updateMap();
        }else
        {
            panel.SetActive(false);
        }

        mapcircle();

        
       

        //centerx=(1280.0f*(1.0f-zoom))*0.5f;
        //centery=(720.0f*(1.0f-zoom))*0.5f;
        //map.GetComponent<RectTransform>().sizeDelta=    win.Locate(new Vector2(1280.0f/zoom,1280.0f/zoom));
        //map.GetComponent<RectTransform>().sizeDelta=    win.Locate(new Vector2(1280.0f,1280.0f));
        //map.GetComponent<RectTransform>().position=     win.LocateVector3(new Vector2(0.0f,720.0f+280.0f));



        //(IN.texcoord/_Zoom+offset)+center+0.5f)
        //map.GetComponent<RectTransform>().position=     win.LocateVector3(new Vector2(((1280.0f/zoom)*(offx-0.5f))-centerx,((720.0f/zoom)*(offy*1.777f+0.5f)-centery)));
    }
    void updateMap()
    {
        set(player.position.x,player.position.z);

        zoom+=Input.GetAxis("Mouse ScrollWheel")/5.0f;
        if(zoom>1.0f)
            zoom=1.0f;
        else if(zoom<0.0f)
            zoom=0.0f;

        Material mat = Instantiate(map.GetComponent<Image>().material);
        mat.SetFloat("_Zoom", (zoom+0.3f)*2.0f);
        mat.SetFloat("_OffsetX", offx);
        mat.SetFloat("_OffsetY", offy);

        //mat.SetFloat("_pointX", (float)2.315 - pointPos.x);
        //mat.SetFloat("_pointY", (float)1.263 - pointPos.y);

        mat.SetFloat("_pointX", pointPos.x);
        mat.SetFloat("_pointY", pointPos.y);

        map.GetComponent<Image>().material = mat;       

    }

    

    public void set(float nowX,float nowY)
    {
        pointPos=MapPoint.instance.playerpositionPoint(nowX,nowY);
    }
    public void move(float x,float y)
    {
        //Debug.Log(centerx+centery);
        offx+=x;
        offy-=y;

        if(offx<0.0f)
            offx=0.0f;
        else if(offx>1.0f)
            offx=1.0f;

        if(offy<0.0f)
            offy=0.0f;
        else if(offy>1.0f)
            offy=1.0f;
    }

    void mapcircle()
    {
        Material mat = Instantiate(circle.GetComponent<Image>().material);

        pointPos=MapPoint.instance.playerCirclePosition(player.position.x,player.position.z);

        mat.SetFloat("_OffsetX", pointPos.x);
        mat.SetFloat("_OffsetY", pointPos.y);
        mat.SetFloat("_Zoom", (zoom+0.3f)*10.0f);

        circle.transform.rotation = Quaternion.Euler(0, 0, 360f-(player.rotation.eulerAngles.y-180f));
        back.transform.rotation = Quaternion.Euler(0, 0, 0);

        circle.GetComponent<Image>().material = mat;       
    }
}
