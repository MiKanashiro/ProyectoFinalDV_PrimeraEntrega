using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonZombie : EnemyController
{

    private void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        movement = Animator.StringToHash("Running");
    }

    void Update()
    {
        if (canSeePlayer)
        {
            LookAt();
            MoveTowards();

            StartCoroutine(SoundRoutine(1));
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
