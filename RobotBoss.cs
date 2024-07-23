using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotBoss : MonoBehaviour
{
    public NavMeshAgent BossNavMeshAgent;
    PlayerController playerController;
    public float damage = 1;
    public Transform playerTransform;
    [SerializeField] Rigidbody projectilePrefab;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform ultPoint;
    [SerializeField] Transform ultPoint1;
    [SerializeField] Transform ultPoint2;
    [SerializeField] Transform ultPoint3;
    [SerializeField] Transform ultPoint4;
    [SerializeField] Transform ultPoint5;
    [SerializeField] Transform ultPoint6; 
    [SerializeField] Transform ultPoint7; 
    [SerializeField] float speed = 50;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource cannon;
    [SerializeField] AudioSource ultiSound;
    float timeSinceLastShot = 0;
    [SerializeField] float shootInterval = 10;
    [SerializeField] float ultimateShootInterval = 15;
    public float health = 400;
    float timeSinceLastUlt;
    Animator animator;
    bool hasShot;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource.Play();
        BossNavMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(Rage());
        DealDamage.width = 792;
    }

    // Update is called once per frame
    void Update()
    {

        var directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = lookRotation;

        BossNavMeshAgent.SetDestination(playerTransform.position);
        timeSinceLastShot += Time.deltaTime;
        timeSinceLastUlt += Time.deltaTime;
        if (timeSinceLastShot >= shootInterval)
        {
            StartCoroutine(CannonShoot());
            timeSinceLastShot = 0.0f; // Reset the timer
            timeSinceLastUlt = 10;
        }
        //StartCoroutine(CannonShoot());
        if(timeSinceLastUlt >= ultimateShootInterval)
        {
            StartCoroutine(Ultimate());
            timeSinceLastUlt = 0.0f;
        }
        if(health <= 0)
        {
            Destroy(gameObject);
            
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.health -= damage;

        }
        if(collision.gameObject.CompareTag("Bullet"))
        {
            health -= 1;
            Debug.Log("Boss health: " + health);
        }
    }
    IEnumerator CannonShoot()
    {
        audioSource.Stop();
        cannon.Play();

        Debug.Log("has Started");
        Debug.Log(health);
        BossNavMeshAgent.isStopped = true;
        animator.SetBool("IsShooting", true);
        yield return new WaitForSeconds(1.5f);
        ShootCannon();
        
        animator.SetBool("IsShooting", false);
        BossNavMeshAgent.isStopped = false;
        cannon.Stop();
        audioSource.Play();





    }
    IEnumerator Ultimate()
    {
        audioSource.Stop();
        ultiSound.Play();
        
        BossNavMeshAgent.isStopped = true;
        animator.SetBool("IsBombing", true);
        yield return new WaitForSeconds(1.5f);
        Shoot(ultPoint);
        Shoot(ultPoint1);
        Shoot(ultPoint2);
        Shoot(ultPoint3);
        Shoot(ultPoint4);
        Shoot(ultPoint5);
        Shoot(ultPoint6);
        Shoot(ultPoint7);
        animator.SetBool("IsBombing", false);
        BossNavMeshAgent.isStopped = false;
        ultiSound.Stop();
        audioSource.Play();
    }
    IEnumerator Rage()
    {
        yield return new WaitForSeconds(50);
        RocketGo.bounceTillDeath = 20;
        damage = 2;
        BossNavMeshAgent.speed = 8.5f;
    }

    void Shoot(Transform point)
    {
        Instantiate(projectilePrefab, point.position, point.rotation);
        
    }
    void ShootCannon()
    {
        Instantiate(projectile, firePoint.position, transform.rotation);
    }
}
