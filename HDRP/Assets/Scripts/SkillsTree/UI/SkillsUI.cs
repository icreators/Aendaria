using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillsUI : MonoBehaviour
{
    public bool isVisible=true;

    private GameObject skillSample;
    private GameObject lineSample;
    private GameObject list;
    private GameObject pkt;
    private GameObject target;
    private SkillsOne oneskill;

    private SkillsWindow win=new SkillsWindow();

    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public int type=0;
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!   

    void Update()
    {
        if (Input.GetButtonDown("SkillsTree"))
        {
            Debug.Log("Tak, kliknięto");

            if(InterfaceManager.instance.isAnyActiveInterface&&isVisible)
                InterfaceManager.instance.isAnyActiveInterface=false;
            
            if(!InterfaceManager.instance.isAnyActiveInterface||isVisible)
                Open();

            if(isVisible)
                InterfaceManager.instance.isAnyActiveInterface=true;
        }
        if(isVisible)
        {
            if(win.ResolutionCheck())
                Generate();
        }
    }
    void Start()
    {
        skillSample=GameObject.Find("SkillsTree/Panel/SkillSample");
        lineSample=GameObject.Find("SkillsTree/Panel/LineSample");
        list=GameObject.Find("SkillsTree/Panel/List");
        pkt=GameObject.Find("SkillsTree/Panel/Pkt");
        target=GameObject.Find("SkillsTree/Panel/Target");
        oneskill=this.GetComponent<SkillsOne>();
        skillSample.active=false;
        lineSample.active=false;
        Open();
    }
    public void Open()
    {
        //if(isVisible)
        //    Remove();
        //else
        //    Generate();
        isVisible=!isVisible;
        this.GetComponent<Canvas>().enabled =isVisible;
        if(isVisible)
            Generate();
        else
            Remove();
    }
    public void Generate()
    {
        SkillsOne copy=this.GetComponent<SkillsOne>().skill[type];          //pobieranie wyglądu drzewa
        SkillsLevelSegment level=this.GetComponent<SkillsLevel>().levels[type];    
                                                                            //wczytywanie informacji o poziomach

        StartCenterTree();                                                  //zerowanie pozycji
        PosTextPkt(level.points+"pkt");                                     //ustawianie pozycji tekstu z punktami
        Remove();                                                           //usuwanie elementów
        this.GetComponent<SkillsBackground>().Reset(type.ToString());       //zmiana tła
        
        target.GetComponent<SkillsTargetSample>().set(level.name+" Level "+level.level,level.procent());
                                                                            //zmiana tekstu

        GenerateLine(0,0,copy.skill);                                       //rysowanie drzewa
        CenterTree(copy.GetTheLongest());                                   //centrowanie drzewa
    }
    private void StartCenterTree()
    {
        list.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2(0,0));
    }
    private void CenterTree(int loong)
    {
        list.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2((1280/2)-(((loong)*64)/2),0));
    }
    private void PosTextPkt(string text)
    {
        pkt.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2(1280-200,0));
        pkt.GetComponent<TextMeshProUGUI>().text=text;
    }
    private void GenerateLine(int y,int x,List<SkillsOne> skillList)
    {
        int loong=0;
        int actx=0;
        foreach (SkillsOne skill in skillList)
        {
            int l=skill.GetTheLongest();
            if(l!=0)
                l--;

            GenerateOne(x+loong+actx,y,skill);
            if(skill.skill.Count>0)
                GenerateLine(y+1,x+loong+actx,skill.skill);
            loong+=l;
            actx++;
        } 
    }
    private float CalculateRest(SkillsOne skill)
    {
        float rest_a=skill.GetTheLongest();

        if(rest_a!=0)
            rest_a--;
        rest_a=rest_a*0.5f;

        return rest_a;
    }
    private void GenerateOne(int x, int y, SkillsOne skill)
    {
        int longest=skill.GetTheLongest();
        if(longest!=0)
            longest--;

        GenerateLine(x+((longest)*0.5f),(y*2));
        if(longest>0)
        {
            float rest_a=CalculateRest(skill.skill[0]);
            float rest_b=CalculateRest(skill.skill[skill.skill.Count-1]);

            GenerateLine(x+rest_a,y*2,longest-rest_a-rest_b);
        }
        if(skill.GetTheLongest()>0)
            GenerateLine(x+((longest)*0.5f),(y*2),0.0f,true);
    
        GameObject element=Instantiate(skillSample);
        element.transform.SetParent(list.transform);
        element.active=true;
        element.GetComponent<SkillsElement>()   .SetInfo(skill.name,skill.info,skill.fullName,skill.pkt);
        element.GetComponent<SkillsElement>()   .SetImage(skill.name);

        Color kolor=new Color();
        if(skill.activated)
        {
            kolor=new Color(0,0,1);
        } 
        else
        {
            if(CanBeSelected(skill)&&this.GetComponent<SkillsLevel>().levels[type].points>=skill.pkt)
                kolor=new Color(1,1,0);
            else
                kolor=new Color(1,1,1);
        }
            
        
        element.GetComponent<SkillsElement>()   .SetColor(kolor);

        element.GetComponent<SkillsElement>()   .Pos(x+((longest)*0.5f),(y*2));
    }
    private void GenerateLine(float x,float y,float w=0.0f,bool up=false)
    {
        GameObject element=Instantiate(lineSample);
        element.transform.SetParent(list.transform);

        element.active=true;
        if(w==0.0f)
        {
            if(!up)
                element.GetComponent<SkillsLine>().SetAsVertically(x,y);
            else
                element.GetComponent<SkillsLine>().SetAsVerticallyUp(x,y);
        }
        else
            element.GetComponent<SkillsLine>().SetAsHorizontally(x,y,w);

    }

    public bool NewActive(string fullN,int pkt)
    {
        SkillsOne copy=this.GetComponent<SkillsOne>();
        SkillsLevelSegment level=this.GetComponent<SkillsLevel>().levels[type];

        if(level.points>=pkt)
        {
            if(this.GetComponent<SkillsOne>().NewActive(fullN))
            {
                level.points-=pkt;
                Generate();
                return true;
            }
        }
        
        return false;
    }

    public void Remove()
    {
        foreach (Transform child in list.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public bool CanBeSelected(SkillsOne one)
    {
        return this.GetComponent<SkillsOne>().get(one.Last()).activated;
    }

}
