using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWindow : MonoBehaviour
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
    public float cut(float act,float max,float min)
    {
        if(act>max)
            return max;
        if(act<min)
            return min;
        return act;
    }
}
