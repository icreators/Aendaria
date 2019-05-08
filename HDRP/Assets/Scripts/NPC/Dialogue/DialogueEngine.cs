using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DialogueEngine : MonoBehaviour
{
    public AudioSource Voices;
    public bool visible=false;
    public string ID;

    bool last_ID;

    dial isPlaying;
    DialogueFile file=new DialogueFile();
    int wait=0;

    private GameObject panel;

    void Start()
    {
        file.NewINI();
        panel=GameObject.Find("Dial/panel");
        Voices=GameObject.Find("Voices").GetComponent<AudioSource>();
    }
    public bool once(string act)
    {
        if(last_ID&&act==ID)
        {
            last_ID=false;
            return true;
        }
        return false;
    }
    public void StartDialogue(string act)
    {
        PlayDialogue(act);
    }
    public string ActualText()
    {
        return isPlaying.content;
    }

    void Update()
    {
        /* TESTOWY DIALOG
        if (Input.GetKeyDown("space"))
        {
            PlayDialogue("1");
        }*/

        if(visible)
        {
            panel.active=true;
            if(Voices.isPlaying)
                wait=0;
            else
                wait++;

            if(wait>2||Input.GetMouseButtonDown(1))
            {
                Voices.Stop();
                set(isPlaying.next);
                wait=0;
            }
        }else
        {
            panel.active=false;
        }
    }
    public void set(string to)
    {
        visible=false;
        if(to!="end")
            PlayDialogue(to);
        else
            ID="";
    }

    void PlayDialogue(string act)
    {
        if(!visible)
        {
            isPlaying=file.load(act);

            if(isPlaying.decide=="yes")
            {
                this.GetComponent<DecideEngine>().Decide(act);
            }

            if(isPlaying.voice!="")
            {
                AudioClip voice=Resources.Load<AudioClip>("Voices/"+isPlaying.voice);
                Voices.PlayOneShot(voice);
            }

            visible=true;
            ID=act;
            last_ID=true;
        }
    }
    

}
