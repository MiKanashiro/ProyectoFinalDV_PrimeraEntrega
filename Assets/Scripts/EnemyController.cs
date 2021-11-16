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


    public GameObject player;


    void Awake()
    {
        animator = GetComponent<Animator>();
        movement = Animator.StringToHash("Movement");
    }

    // Update is called once per frame
    void Update()
    {
        LookAt();
        MoveTowards();
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
}
