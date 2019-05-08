using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsLine : MonoBehaviour
{
    private SkillsWindow win=new SkillsWindow();
    
    private float size_x = 64;
    private float size_y = 64;

    public void SetAsVertically(float x,float y)//pionowo
    {
        Set(    (x*size_x)+(size_x/2)   ,   y*size_y-(size_y/2)    ,   4    ,   size_y);
    }
    public void SetAsVerticallyUp(float x,float y)//pionowo
    {
        Set(    (x*size_x)+(size_x/2)   ,   y*size_y+(size_y/2)    ,   4    ,   size_y);
    }
    public void SetAsHorizontally(float x,float y,float w)//poziomo
    {
        Set(    (x*size_x)+(size_x/2)   ,   (y*size_y)+(size_y*1.5f)    ,   (w*size_x)    ,   4);
    }
    private void Set(float x,float y,float w,float h)
    {
        this.GetComponent<RectTransform>().sizeDelta=win.Locate(new Vector2(w,h));
        this.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2(x,y));
    }
}
