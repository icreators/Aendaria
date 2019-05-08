using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public bool IsVisible()
    {
        return this.GetComponent<DialogueEngine>().visible;
    }
    public void StartDialogue(string act)
    {
        this.GetComponent<DialogueEngine>().StartDialogue(act);
    }
    public string ActualText()
    {
        return this.GetComponent<DialogueEngine>().ActualText();
    }
    
    public string ID()
    {
        return this.GetComponent<DialogueEngine>().ID;
    }
    public bool Once(string ID)
    {
        return this.GetComponent<DialogueEngine>().once(ID);  
    }
}
