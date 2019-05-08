using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Animator animator;

    public Canvas health;

    private void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public override void Die()
    {
        base.Die();

        isDied = true;

        animator.ResetTrigger("BaseAttack");

        animator.SetInteger("Death", Random.Range(1, 3));

        Destroy(health);

        SkillsManager.instance.AddExp(1, 100f);

        // Add triumph animation to enemy

        //Destroy(gameObject);
    }
}
