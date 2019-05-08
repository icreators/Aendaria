using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public bool isDied = false;

    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    [SerializeField] Canvas ui;

    [SerializeField] Text healthText;

    //[SerializeField] Slider healthSlide;
    [SerializeField] Slider healthSlideBG;

    private Animator animator;

    float currentWidth = 100;

    void Awake()
    {
        animator = gameObject.GetComponentInParent<Animator>();

        //healthSlide.maxValue = maxHealth;
        healthSlideBG.maxValue = maxHealth;

        currentHealth = maxHealth;
        //healthSlide.value = currentHealth;
        healthSlideBG.value = currentHealth;

        //ui.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) TakeDamage(10);
    }

    public void TakeDamage (int damage)
    {
        currentWidth = currentHealth;

        damage -= armor.GetValue();

        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        ChangeDmg();

        if (currentHealth <= 0)
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
        if (currentHealth < 0) currentHealth = 0;

        //healthSlide.value = currentHealth;
        if (healthText != null) healthText.text = currentHealth.ToString();
        
        StartCoroutine(HealthAnim());
    }

    IEnumerator HealthAnim()
    {
        while (currentWidth >= currentHealth)
        {
            yield return new WaitForSeconds(0.05f);

            healthSlideBG.value = currentWidth;

            currentWidth -= 0.8f;
        }
    }
}
