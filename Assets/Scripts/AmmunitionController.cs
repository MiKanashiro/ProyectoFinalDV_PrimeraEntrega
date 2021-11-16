using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionController : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    [SerializeField] private Transform player;
    float spaceX;
    float spaceZ;

    private void Start()
    {
        Vector3 space = plane.GetComponent<Collider>().bounds.size - GetComponent<Collider>().bounds.size;
        spaceX = (space.x - transform.localScale.x) / 2;
        spaceZ = (space.z - transform.localScale.z - transform.localScale.x) / 2;
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisionamos!");
            Invoke("MoveAnotherPosition", 0.5f);
        }       
    }
    void MoveAnotherPosition()
    {
        transform.position = new Vector3(Random.Range(-spaceX, spaceX), transform.position.y, Random.Range(-spaceZ, spaceZ));
        var lookPos = player.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
