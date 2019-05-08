using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsBackground : MonoBehaviour
{
    private GameObject backPanel;
    private GameObject frontPanel;
    private float alpha=0;

    private string backimage;
    void Start()
    {
        frontPanel=GameObject.Find("SkillsTree/Panel/FrontPanel");
        backPanel=GameObject.Find("SkillsTree/Panel");
    }

    public void Reset(string image)
    {
        if(backimage!=image)
        {
            backimage=image;
            frontPanel.GetComponent<Image>().sprite=Resources.Load<Sprite>("Skills/Background/"+image);
            alpha=0;
        }
    }

    void Update()
    {
        if(alpha<1.0f)
        {
            frontPanel.GetComponent<Image>().color=new Color(1,1,1,alpha);
            alpha+=0.1f;
        }else
        {
            backPanel.GetComponent<Image>().sprite  =  frontPanel.GetComponent<Image>().sprite;
        }
    }
}
