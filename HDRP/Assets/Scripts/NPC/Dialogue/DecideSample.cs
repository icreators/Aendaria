using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecideSample : MonoBehaviour
{
    public string next;
    public void set(string act_text, string act_next)
    {
        next=act_next;
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=act_text;

        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(this.OnClick);
    }
    public void OnClick()
    {
        GameObject.Find("Dial").GetComponent<DialogueEngine>().set(next);
        GameObject.Find("Dial").GetComponent<DecideEngine>().RemoveList();
    }
}
