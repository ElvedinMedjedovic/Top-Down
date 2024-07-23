using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Rigidbody teleportProjectile;
    public float speed = 10.0f;
    bool canShoot = true;
    [SerializeField] ParticleSystem teleportElectric;
    
    [SerializeField] GameObject player;
    [SerializeField] AudioSource audioS;
    [SerializeField] Animator animator;
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && canShoot)
        {


            audioS.Play();
            animator.Play("Aim Teleport");
            Rigidbody instantiatedProjectile = Instantiate(teleportProjectile, transform.position, transform.rotation) as Rigidbody;

            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            canShoot = false;


            teleportElectric.Play();
            StartCoroutine(ShootCoolDown());

        }

    }
    IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(3);
        
        canShoot = true;
    }
    



}