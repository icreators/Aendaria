using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsButton : MonoBehaviour
{
    public int type=0;
    private GameObject tree;
    private SkillsWindow win=new SkillsWindow();

    void Start()
    {
        tree=GameObject.Find("SkillsTree");
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void Update()
    {
        if(tree.GetComponent<SkillsUI>().isVisible)
            Pos();
    }

    void OnClick()
    {
        tree.GetComponent<SkillsUI>().type=type;
        tree.GetComponent<SkillsUI>().Generate();
    }
    private void Pos()
    {
        this.GetComponent<RectTransform>().sizeDelta=win.Locate(new Vector2(100,100));
        this.GetComponent<RectTransform>().position=win.LocateVector3(new Vector2(type*100,720-100));
    }
}
