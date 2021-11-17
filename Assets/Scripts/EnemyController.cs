using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float speedEnemy = 5f;
    [SerializeField]
    float speedToLook = 2f;
    private Animator animator;
    private int movement;
    [SerializeField]
    float currentHeal = 100f;

    public LayerMask targetMask;
    private LayerMask obstrucionMask;
    private GameObject player;

    [SerializeField]
    private bool canSeePlayer;
    [SerializeField]
    private float radius = 7;
    [SerializeField]
    [Range(0,360)]
    private int angle = 100;

    void Awake()
    {
        animator = GetComponent<Animator>();
        movement = Animator.StringToHash("Movement");
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }
 
    void Update()
    {
        if (canSeePlayer)
        {
            LookAt();
            MoveTowards();
        }
    }

    private void MoveTowards()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speedEnemy * Time.deltaTime);
        animator.Play(movement);
    }

    private void LookAt()
    {
        Quaternion newRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
    //FOV = field of view 
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

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
