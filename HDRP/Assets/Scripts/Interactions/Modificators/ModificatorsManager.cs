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

        playerStats = GameObject.Find("Player").transform.GetComponent<PlayerStats>();
    }

    #endregion

    public PlayerStats playerStats;

    public void ChangeModificator(string name, int howMuch, int duration = 0, bool incrementally = false, bool modification = true)
    {
        if (duration == 0) //forever
        {
            ChangeM(name, howMuch, modification);
        }
        else if (incrementally)
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

    void ChangeM(string name, int howMuch, bool mod = true)
    {
        if (mod)
        {
            playerStats.ChangeModificator(name, howMuch);
        }
        else
        {
            playerStats.ChangeCurrentStat(name, howMuch);
        }
    }
}