using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    private NavMeshAgent myAgent;
    public Transform target;
    private Animator myAnimator;

    public bool chaseTarget = true;
    public bool searchTarget = true;
    public bool attackTarget = true;

    public float stoppingDistance = 2.5f;
    public float delayBetweenAttacks = 1.5f;
    private float attackCooldown;

    private float distanceFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.stoppingDistance = stoppingDistance;
        attackCooldown = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        ChaseTarget();
    }

    void ChaseTarget()
    {
        distanceFromTarget = Vector3.Distance(target.position, transform.position);
        if(distanceFromTarget > stoppingDistance)
        {
            chaseTarget = true;
        }
        else
        {
            chaseTarget = false;
            Attack();
        }

        if (chaseTarget)
        {
            myAgent.SetDestination(target.position);
            myAnimator.SetBool("isSearching", false);
            myAnimator.SetBool("isChasing", true);
        }
        else
        {
            myAnimator.SetBool("isChasing", false);
            myAnimator.SetBool("isSearching", true);
        }
    }

    void Attack()
    {
        if(Time.time > attackCooldown)
        {
            Debug.Log("Attack");
            myAnimator.SetTrigger("AttackTrigger");
            attackCooldown = Time.time + delayBetweenAttacks;
        }
    }
}
