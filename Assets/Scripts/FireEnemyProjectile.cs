using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemyProjectile : MonoBehaviour {
    private Transform playerTransform;
    public float projectileSpeed;
    public float projectileDamage;
    public Rigidbody rigidbody1;

    void Awake()
    {
        projectileSpeed = 200.0f;
        projectileDamage = 50.0f;
        rigidbody1 = GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Player").transform;
    }

    void Start()
    {
        rigidbody1.AddForce((playerTransform.transform.position - rigidbody1.position).normalized 
                             * projectileSpeed);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerController>().health -= projectileDamage;
            Debug.Log("Hit");
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
