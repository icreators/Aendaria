using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestListElement : MonoBehaviour
{
    public string title;
    public string id_quest;


    public void set(string name,string newIdQuest)
    {
        id_quest=newIdQuest;
        title=name;
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=title;

        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        GameObject.Find("QuestsUI").GetComponent<QuestUI>().TargetUI(id_quest);
    }
}
