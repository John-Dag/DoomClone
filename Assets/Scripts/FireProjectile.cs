using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour {
    public Transform startPosition;
    public float projectileSpeed;
    public float projectileDamage;
    public Rigidbody rigidbody1;

    void Awake()
    {
        projectileSpeed = 15000.0f;
        projectileDamage = 50.0f;
        rigidbody1 = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rigidbody1.AddForce(rigidbody1.transform.forward * projectileSpeed);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<ImpController>().health -= projectileDamage;
            Debug.Log("Hit");
            Destroy(this.gameObject);
        }

        if (col.gameObject.tag == "Level")
        {
            Destroy(this.gameObject);
        }
    }
}
