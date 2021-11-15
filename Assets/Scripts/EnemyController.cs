using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speedEnemy = 1f;
    float speedToLook = 2f;
    public GameObject player;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position - transform.position).magnitude > 2)
        {
            LookAt();
            MoveTowards();
        }
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
}
