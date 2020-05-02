using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHp = 100;
    private int currentHp;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        animator = GetComponent<Animator>();
    }

    public void TakeDamageHeadshot(int _damage)
    {
        currentHp -= _damage * 3;
        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("DamageTrigger");
        }
    }

    public void TakeDamage(int _damage)
    {
        currentHp -= _damage;
        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("DamageTrigger");
        }
    }

    public void Die()
    {
        animator.SetTrigger("DyingTrigger");
        animator.SetBool("isDying", true);
        StartCoroutine("WaitForAnimationEnd");
    }

    public IEnumerator WaitForAnimationEnd()
    {
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine("WaitForDestroy");
    }

    public IEnumerator WaitForDestroy()
    {
        yield return new WaitForSecondsRealtime(1);
        //Destroy(gameObject);
    }
}
