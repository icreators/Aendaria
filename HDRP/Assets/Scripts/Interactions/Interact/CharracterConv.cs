using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharracterConv : Interactable
{
    public string ID;

    public override void Interact()
    {
        base.Interact();
        Conversation();
    }

    void Conversation()
    {
        Debug.Log("Conversation with " + gameObject.name);
        
        GameObject.Find("Dial").GetComponent<DialogueManager>().StartDialogue(ID);
    }
}
