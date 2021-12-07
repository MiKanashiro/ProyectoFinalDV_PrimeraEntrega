using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onHit;

    [SerializeField]
    private GameObject bulletDecal;

    private float speed = 50f;
    private float timeToDestroy = 1f;

    public Vector3 target { get; set; }
    public bool hit { get; set; }
    

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(!hit && Vector3.Distance(transform.position, target) < 0.01f)
        {
            Destroy(gameObject,2f);
        }
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);

        // todo destroy instance with body 
        GameObject.Instantiate(bulletDecal, contact.point + contact.normal * 0.001f, Quaternion.LookRotation(contact.normal)); 
        
        if (collision.gameObject.CompareTag("Enemy"))
        {

            //GameManager.Instance.addScore();
            //print("Score: " + GameManager.Instance.getScore());
            onHit?.Invoke();
            Destroy(collision.gameObject);
            
        }
    }
}
