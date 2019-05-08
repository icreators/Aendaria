using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct dial{
    public string content,voice,next,decide;
};

public class DialogueFile : MonoBehaviour
{
    INIParser ini = new INIParser();
    
    public void NewINI()
    {
        ini.Open(Application.dataPath+"/dialogue.ini");
    }
    public dial load(string act)
    {
        dial newDial=new dial();
        newDial.content=    ini.ReadValue(act,"text","end");
        newDial.voice=      ini.ReadValue(act,"voice","");
        newDial.next=       ini.ReadValue(act,"next","");
        newDial.decide=     ini.ReadValue(act,"decide","no");
        return newDial;
    }
    public string get(string act,string vol)
    {
        return ini.ReadValue(act,vol,"");
    }

}
