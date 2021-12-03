using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    protected float speedEnemy = 3f;
    [SerializeField]
    protected float speedToLook = 2f;
    protected Animator animator;
    protected int movement;
    [SerializeField]
    protected float currentHeal = 100f;

    public LayerMask targetMask;
    protected LayerMask obstrucionMask;
    protected GameObject player;

    [SerializeField]
    protected bool canSeePlayer;
    [SerializeField]
    protected float radius = 7;
    [SerializeField]
    [Range(0,360)]
    protected int angle = 100;

    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    //bullets
    public void takeDamage(float damage)
    {
        currentHeal -= damage;
    }

    //zombies eat
    public void healing(float heal)
    {
        currentHeal += heal;

    }

    //FOV = field of view 
    protected IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    protected void FieldOfViewCheck()
    {

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        print(rangeChecks);
        if (rangeChecks.Length != 0)
        {
            //one layer to search
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float disntanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, disntanceToTarget, obstrucionMask))
                {
                    canSeePlayer = true;
                }
                else
                    canSeePlayer = false;
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer) canSeePlayer = false;
    }
}
