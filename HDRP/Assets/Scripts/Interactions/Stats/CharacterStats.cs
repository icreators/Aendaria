using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public bool isDied = false;

    [SerializeField] Canvas ui;
    [SerializeField] Text healthText;
    [SerializeField] Slider healthSlide;
    [SerializeField] Slider healthSlideBG;

    private Animator animator;

    float currentWidth = 100;

    #region Modificators

    Dictionary<string, int> modificatorType = new Dictionary<string, int>();
    static Dictionary<string, int> basePlayerStats = new Dictionary<string, int>();
    Dictionary<string, int> currentPlayerStats = new Dictionary<string, int>();

    void Awake()
    {
        modificatorType.Add("maxHealth", 1);
        modificatorType.Add("armor", 1);
        modificatorType.Add("fightStrength", 1);
        modificatorType.Add("magicFightStrength", 1);
        modificatorType.Add("moveSpeed", 1);

        basePlayerStats.Add("MaxHealth", 100);
        basePlayerStats.Add("FightStrength", 10);
        basePlayerStats.Add("ArmorFromClothes", 0);
        basePlayerStats.Add("MagicFightStrength", 0);
        basePlayerStats.Add("MoveSpeed", 6);

        currentPlayerStats.Add("maxHealth", basePlayerStats["MaxHealth"]);
        currentPlayerStats.Add("health", basePlayerStats["MaxHealth"]);
        currentPlayerStats.Add("armor", basePlayerStats["ArmorFromClothes"]);
        currentPlayerStats.Add("fightStrength", basePlayerStats["FightStrength"]);
        currentPlayerStats.Add("magicFightStrength", basePlayerStats["MagicFightStrength"]);
        currentPlayerStats.Add("moveSpeed", basePlayerStats["MoveSpeed"]);

        animator = gameObject.GetComponentInParent<Animator>();

        healthSlide.maxValue = currentPlayerStats["maxHealth"];
        healthSlideBG.maxValue = currentPlayerStats["maxHealth"];

        healthSlide.value = currentPlayerStats["health"];
        healthText.text = currentPlayerStats["health"].ToString();

        currentPlayerStats["health"] = currentPlayerStats["maxHealth"];
        //ui.gameObject.SetActive(true);
    }

    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) TakeDamage(10);
    }

    public int GetCurrentStat(string modName)
    {
        return currentPlayerStats[modName];
    }

    public void ChangeCurrentStat(string modName, int value)
    {
        currentPlayerStats[modName] += value;
        //StatsRefresh(modName);
        ChangeDmg();
    }

    public void ChangeModificator(string modName, int value)
    {
        modificatorType[modName] += value;
        StatsRefresh(modName);
    }

    private void StatsRefresh(string modName)
    {
        currentPlayerStats[modName] = basePlayerStats[modName] *= modificatorType[modName];
    }

    public void TakeDamage (int damage)
    {
        currentWidth = currentPlayerStats["health"];

        damage -= currentPlayerStats["armor"];

        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentPlayerStats["health"] -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        ChangeDmg();

        if (currentPlayerStats["health"] <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //isDied = true;

        

        Debug.Log(transform.name + "died.");
    }

    public void ChangeDmg()
    {
        if (currentPlayerStats["health"] < 0) currentPlayerStats["health"] = 0;

        healthSlide.value = currentPlayerStats["health"];
        if (healthText != null) healthText.text = currentPlayerStats["health"].ToString();
        
        StartCoroutine(HealthAnim());
    }

    IEnumerator HealthAnim()
    {
        while (currentWidth >= currentPlayerStats["health"])
        {
            yield return new WaitForSeconds(0.05f);

            healthSlideBG.value = currentWidth;

            currentWidth -= 0.8f;
        }
    }
}
