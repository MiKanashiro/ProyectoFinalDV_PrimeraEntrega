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
    [Range(0, 360)]
    protected int angle = 100;

    [Header("Audio")]
    [SerializeField]
    protected AudioClip[] zombieClip;
    [SerializeField]
    protected AudioSource source;
    [SerializeField]
    protected Vector2 audioPitch = new Vector2(.9f, 1.1f);
    protected bool isChasign;

    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());

    }

    protected void Start()
    {
        if (source != null)
        {
            source.clip = zombieClip[0];
            source.Play();
        }
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

    protected IEnumerator SoundRoutine(int index, int delay = 0)
    {
        WaitForSeconds wait = new WaitForSeconds(zombieClip[index].length);
        while (true)
        {
            yield return wait;
            PlayAudioClip(index, delay);
        }
    }

    protected void PlayAudioClip(int index, int delay = 0)
    {
        if(!source.isPlaying)
        {
            if (index >= 0 || index < zombieClip.Length)
            {
                source.clip = zombieClip[index];
                source.PlayDelayed(delay);
            }

        }
    }
    
    protected void FieldOfViewCheck()
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

                    source.Play();
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
