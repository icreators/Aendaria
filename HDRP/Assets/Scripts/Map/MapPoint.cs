using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MapPoint : MonoBehaviour
{
    #region Singleton
    public static MapPoint instance;

    private void Awake()
    {
        if (instance != null) Debug.LogWarning("More than one instance of MapPoint found!");

        instance = this;
    }
    #endregion

    //public float shaderPointPrecisionXStart=2.315f;
    //public float shaderPointPrecisionXEnd=1.315f;

    //public float shaderPointPrecisionYStart=1.262f;       1.375f          0,113 diffrence
    //public float shaderPointPrecisionYEnd=0.485f;         0.375f          0,113 diffrence
    //cropp 0,774f


    public float gamePlayerPrecisionXStart;
    public float gamePlayerPrecisionXEnd;
    public float gamePlayerPrecisionYStart;
    public float gamePlayerPrecisionYEnd;

    public float Xdiffrence=0.315f;
    public float Ydiffrence=0.375f;

    public Vector2 playerpositionPoint(float nowX,float nowY)
    {
        //return playerPosition( nowX, nowY)+new Vector2(Xdiffrence,Ydiffrence);
        return playerPosition( nowX, nowY);
    }

    public Vector2 playerPosition(float nowX,float nowY)
    {
        Vector2 pos=new Vector2();
        pos.x=procent(gamePlayerPrecisionXStart,gamePlayerPrecisionXEnd,nowX);
        //pos.y=0.113f+procent(gamePlayerPrecisionYStart,gamePlayerPrecisionYEnd,nowY)*0.774f;
        pos.y=(procent(gamePlayerPrecisionYStart,gamePlayerPrecisionYEnd,nowY)*0.774f);
        //compositing

        pos.y=1.265f-pos.y;
        pos.x=2.315f-pos.x;


        return pos;
    }

    public Vector2 playerCirclePosition(float nowX,float nowY)
    {
        Vector2 pos=new Vector2();
        pos.x=procent(gamePlayerPrecisionXStart,gamePlayerPrecisionXEnd,nowX);
        pos.y=(procent(gamePlayerPrecisionYStart,gamePlayerPrecisionYEnd,nowY)*0.774f);
        
        pos.y=pos.y+0.113f;
        return pos;

    }


    public float procent(float from,float to,float now)
    {
        float a=now-from; //aktualna pozycja
        float b=to-from; //maksimum pozycji
        //Debug.Log("from: "+from+" to: "+to+" now: "+now);
        return a/b;
    }

}
