using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestWindow : MonoBehaviour
{
    public Vector2 Locate(Vector2 pos)
    {
        return (pos/new Vector2(1280.0f,720.0f))*new Vector2(Screen.width,Screen.height);
    }
    public Vector3 LocateVector3(Vector2 pos)
    {
        Vector2 actual=Locate(pos);
        return new Vector3(actual.x,actual.y,0);
    }
    public Rect NewSize(float x,float y,float z,float w)
    {
        Rect newRect=new Rect();

        Vector2 newPos=Locate(new Vector2(x,y));
        
        newRect.x=newPos.x;
        newRect.y=newPos.y;

        newPos=Locate(new Vector2(z,w));

        newRect.width=newPos.x;
        newRect.height=newPos.y;

        return newRect;
    }
}
