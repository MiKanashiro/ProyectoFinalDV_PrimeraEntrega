using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildZombie : EnemyController
{
    private int roarAnimationHash;


    private void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        movement = Animator.StringToHash("Running Crawl");
        roarAnimationHash = Animator.StringToHash("Roar");
    }


    void Update()
    {
        
        if (canSeePlayer && !isChasign)
        {
            LookAt();
            animator.Play(roarAnimationHash);
            print(source.isPlaying);
            if (!source.isPlaying)
            {
                PlayAudioClip(2);
            }
        }
        else if(canSeePlayer && isChasign && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            LookAt();
            MoveTowards();
            if (!source.isPlaying)
            {
                PlayAudioClip(6);
            }
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
        isChasign = true;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().subtractPlayerLives();

            if (collision.gameObject.GetComponent<PlayerController>().getPlayerLives() < 1)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
