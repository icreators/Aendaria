using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modificator : ScriptableObject
{
    public string ModificatorName = "Modificator name";
    public string description = "Modificator description";

    public int duration = 10; //seconds

    public Image icon = null;

    Transform ModificatorsTab;

    public virtual void Action()
    {
        Debug.Log("Modyficator <" + ModificatorName + "> !");

        Image mod = Instantiate(icon);
        mod.transform.parent = GameObject.Find("ModificatorsTab").transform;
    }
}
