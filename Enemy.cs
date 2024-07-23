using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent EnemyNavMesh;
    public Transform playerTransform;
    public float enemyHp;
    [SerializeField] float damage = 1;
    [SerializeField] float attackDistance = 35f;
    PlayerController playerController;
    Rigidbody enemyRb;
    [SerializeField] bool isRanged;
    Animator animator;
    [SerializeField] float shootInterval = 1.0f;
    private float timeSinceLastShot = 0.0f;
    [SerializeField] Rigidbody projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float speed = 20.0f;
    [SerializeField] AudioSource audioS;
    //[SerializeField] AudioClip walk;

    // A variable to store the audio state
    private bool isAudioPlaying = false;


    // Start is called before the first frame update
    void Awake()
    {
        EnemyNavMesh = GetComponent<NavMeshAgent>();
        enemyRb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerTransform != null)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = lookRotation;

            if (distance < attackDistance)
            {

                EnemyNavMesh.destination = playerTransform.position;
                animator.SetBool("IsMoving", true);


                if (EnemyNavMesh.stoppingDistance > distance)
                {
                    animator.SetBool("IsMoving", false);

                }


                if (isRanged)
                {
                    timeSinceLastShot += Time.deltaTime;
                    if (timeSinceLastShot >= shootInterval)
                    {
                        Shoot();
                        timeSinceLastShot = 0.0f; // Reset the timer
                    }
                }
            }
            else
            {
                animator.SetBool("IsMoving", false);

            }


        }



        if (enemyHp <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Shoot()
    {
        Rigidbody instantiatedProjectile = Instantiate(projectilePrefab, firePoint.position, transform.rotation) as Rigidbody;
        instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.health -= damage;
            //Vector3 forceDirection = transform.position - playerTransform.position;
            //Vector3 newDestination = transform.position + forceDirection.normalized * 5;

            // Update the NavMeshAgent's destination
            //StartCoroutine(HasTouchedCounter());

            //EnemyNavMesh.SetDestination(newDestination);

        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            enemyHp -= 1;
        }
    }

    // This method is called by the animator component during the normal update cycle
    private void OnAnimatorMove()
    {
        // Check if the animator is moving or not
        if (animator.GetBool("IsMoving"))
        {
            // If the audio is not playing, play it and set the state to true
            if (!isAudioPlaying)
            {
                audioS.Play();
                isAudioPlaying = true;
            }
        }
        else
        {
            // If the audio is playing, stop it and set the state to false
            if (isAudioPlaying)
            {
                audioS.Stop();
                isAudioPlaying = false;
            }
        }
    }



}