using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    [SerializeField] private Transform player;
    float spaceX;
    float spaceZ;
    //bool alreadyInvoke = false;
    private void Start()
    {

        Vector3 space = plane.GetComponent<Collider>().bounds.size - GetComponent<Collider>().bounds.size;
        spaceX = (space.x - transform.localScale.x) / 2;
        spaceZ = (space.z - transform.localScale.z - transform.localScale.x) / 2;
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Colisionamos!");
            Invoke("MoveAnotherPosition", 0.5f);
            //alreadyInvoke = true;
        }       
    }
    void MoveAnotherPosition()
    {
        transform.position = new Vector3(Random.Range(-spaceX, spaceX), transform.position.y, Random.Range(-spaceZ, spaceZ));
        var lookPos = player.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);
        //alreadyInvoke = false;
    }
}
