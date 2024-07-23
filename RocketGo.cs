using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGo : MonoBehaviour
{
    Rigidbody rocketRb;
    PlayerController playerController;
    [SerializeField] GameObject explosion;
    public static int bounceTillDeath = 10;
    int bounce = 0;
    public float damage = 1;
    int force = 71;
    private void Awake()
    {
        rocketRb = GetComponent<Rigidbody>();
        Destroy(gameObject, 10);
    }

    void LateUpdate()
    {
        rocketRb.AddRelativeForce(Vector3.forward * force);
        if(transform.position.y > 2)
        {
            rocketRb.AddForce(Vector3.down * 20);
        }
        
        if(transform.position.y <= 2)
        {
            rocketRb.constraints = RigidbodyConstraints.FreezePositionY;
        }
        transform.forward = rocketRb.velocity.normalized;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.health -= damage;
        }

        bounce += 1;
        if(bounce == bounceTillDeath)
        {
            explosion.SetActive(true);
            force = 0;
            rocketRb.constraints = RigidbodyConstraints.FreezePositionZ;
            rocketRb.constraints = RigidbodyConstraints.FreezePositionX;
            StartCoroutine(WaitForParticle());
            
            Destroy(gameObject, 1);

        }
    }
    IEnumerator WaitForParticle()
    {
        yield return new WaitForSeconds(.45f);
        gameObject.SetActive(false);
    }
}
