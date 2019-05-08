using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueText : MonoBehaviour
{
    private DialogueManager manager;
    void Start()
    {
        manager=GameObject.Find("Dial").GetComponent<DialogueManager>();
    }

    void Update()
    {
        string text=manager.ActualText();
        if(text=="end")
            text=" ";

        this.GetComponent<TextMeshProUGUI>().text=text;
    }
}
