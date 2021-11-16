using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speedEnemy = 0.5f;
    float speedToLook = 2f;
    public GameObject player;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookAt();
        MoveTowards();
    }
    private void MoveTowards()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += speedEnemy * direction * Time.deltaTime;
    }
    private void LookAt()
    {
        Quaternion newRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }
}
