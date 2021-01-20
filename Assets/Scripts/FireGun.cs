using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour {
    public Animator animator;
    public GameObject Gun1ProjectilePrefab;
    public GameObject projectileSpawn;
    private float timer;
    private bool hasFired;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            if (timer >= 1.0f || !hasFired)
            {
                animator.SetBool("isFiring", true);
                GameObject projectile = (GameObject)Instantiate(Gun1ProjectilePrefab, projectileSpawn.transform.position,
                                                                projectileSpawn.transform.rotation);
                hasFired = true;
                timer = 0;
            }
            else
            {
                animator.SetBool("isFiring", false);
            }
        }

        timer += Time.deltaTime;
    }
}
