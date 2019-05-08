using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideEngine : MonoBehaviour
{
    DialogueFile file=new DialogueFile();
    DialogueWindow win=new DialogueWindow();

    private GameObject sample;
    private GameObject list;

    void Start()
    {
        file.NewINI();

        sample=GameObject.Find("Dial/sample");
        sample.active=false;
        list=GameObject.Find("Dial/list");
    }

    public void Decide(string ID)
    {
        int size_y=0;
        string act_text;
        string act_next;

        do{
            act_text=file.get(ID,(size_y+1).ToString());
            act_next=file.get(ID,(size_y+1).ToString()+"-next");

            GameObject element=Instantiate(sample);

            element.active=true;
            element.transform.SetParent(list.transform);

            element.GetComponent<DecideSample>().set((size_y+1)+"."+act_text,act_next);
            element.GetComponent<RectTransform>().sizeDelta=win.Locate(new Vector2(1280,100));
            element.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2(0,(100*(size_y+1))));
                
            size_y++;

            // TO NIŻEJ JEST TYMCZASOWO BO NARAZIE NIE WIEM JAK TO ZROBIĆ INACZEJ XDDDDDD
            act_text=file.get(ID,(size_y+1).ToString());

        }while(act_text!="");
    }
    public void RemoveList()
    {
        foreach (Transform child in list.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
