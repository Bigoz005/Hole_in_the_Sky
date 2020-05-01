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

    public void TakeDamage(int _damage)
    {
        currentHp -= _damage;
        animator.SetTrigger("DamageTrigger");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetTrigger("DyingTrigger");
        StartCoroutine("WaitForAnimationEnd");
    }

    public IEnumerator WaitForAnimationEnd()
    {
        yield return new WaitForSecondsRealtime(1);
        animator.StopPlayback();
        StartCoroutine("WaitForDestroy");
    }

    public IEnumerator WaitForDestroy()
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }
}
