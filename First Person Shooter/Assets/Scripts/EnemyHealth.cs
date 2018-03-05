using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float Health = 10;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if(Health <= 0)
        {
            return;
        }
        Health -= damage;
        if(Health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        animator.SetTrigger("Death");
    }
}
