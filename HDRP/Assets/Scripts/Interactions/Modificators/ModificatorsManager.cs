using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificatorsManager : MonoBehaviour
{
    #region Singlenton

    public static ModificatorsManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    #region Modificators

    Dictionary<string, int> modificatorType = new Dictionary<string, int>();

    public void Avake()
    { 
        modificatorType.Add("health", 1);
        modificatorType.Add("armor", 1);
        modificatorType.Add("damage", 1);
        modificatorType.Add("magicDamage", 1);
        modificatorType.Add("moveSpeed", 1);
    }

    #endregion

    public void ChangeModificator(string name, int howMuch, int duration = 0, bool incrementally = false)
    {
        if (duration == 0) //forever
        {
            ChangeM(name, howMuch);
        }
        if (incrementally)
        {
            StartCoroutine(AddIncrementally(name, howMuch, duration));
        }
        else
        {
            StartCoroutine(ModifierTimer(name, howMuch, duration));
        }
    }

    private IEnumerator AddIncrementally(string name, int howMuch, int duration)
    {
        for (int x=0; x<howMuch*60; x+=60)
        {
            ChangeM(name, howMuch/duration/60);
            yield return new WaitForSeconds(1 / 60);
        }
    }

    private IEnumerator ModifierTimer(string name, int howMuch, int duration)
    {
        ChangeM(name, howMuch);

        yield return new WaitForSeconds(duration);

        ChangeM(name, -howMuch);
    }

    void ChangeM(string name, int howMuch)
    {
        modificatorType[name] += howMuch;
    }
}