using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpController : MonoBehaviour {
    public Animator animator;
    public GameObject projectileSpawn;
    public GameObject projectilePrefab;
    public float health;
    private Transform playerTransform;
    private Vector3 targetDirection;
    private float angleToPlayer;
    private bool hasSpottedPlayer;
    private GameObject collidingObject;
    private CharacterController controller;
    private float attackTimer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        hasSpottedPlayer = false;
        health = 100.0f;
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        //Always face the player
        transform.LookAt(new Vector3(playerTransform.position.x, 0.0f, playerTransform.position.z));
        targetDirection = playerTransform.position - transform.position;
        angleToPlayer = (Vector3.Angle(targetDirection, transform.forward));

        if (animator.GetBool("isDead"))
            return;

        if (health <= 0)
        {
            animator.SetBool("isPursuing", false);
            animator.SetBool("isDead", true);
            GetComponent<CapsuleCollider>().enabled = false;
            controller.enabled = false;

            return;
        }

        //transform.LookAt(transform.position + playerPosition.transform.rotation * Vector3.forward, 
        //                 playerPosition.transform.rotation * Vector3.up);

        //If the player has been spotted begin pursuit
        if (angleToPlayer >= -90 && angleToPlayer <= 90 && !hasSpottedPlayer)
        {
            animator.SetBool("isPursuing", true);
            hasSpottedPlayer = true;
        }

        if (hasSpottedPlayer)
        {
            //If the player is less than a certain distance, stop pursuit.
            if (Vector3.Distance(transform.position, playerTransform.position) <= 1f)
            {
                animator.SetBool("isPursuing", false);
            }
            else
            {
                animator.SetBool("isPursuing", true);

                if (!animator.GetBool("isAttacking"))
                {
                    controller.Move(transform.forward * 1.0f * Time.deltaTime);
                    //transform.position += transform.forward * 1.0f * Time.deltaTime;
                }
            }

            if ((Vector3.Distance(transform.position, playerTransform.position) <= 20f) && 
                (attackTimer >= 5f))
            {
                animator.SetBool("isPursuing", false);
                animator.SetBool("isAttacking", true);

                //StartCoroutine(WaitOnAttack());
            }
            else
            {
                animator.SetBool("isAttacking", false);
            }
        }

        attackTimer += Time.deltaTime;
    }

    void Attack()
    {
        GameObject projectile = (GameObject)Instantiate(projectilePrefab, projectileSpawn.transform.position,
                                                        projectileSpawn.transform.rotation);
        animator.SetBool("isAttacking", false);
        attackTimer = 0f;
    }
}
