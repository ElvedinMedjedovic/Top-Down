using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooter : MonoBehaviour
{
    public Rigidbody projectile;
    public float speed = 20.0f;
    public float continuousFireRate = 5.5f; // Rate when holding down the left mouse button
    private float continuousFireTime;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip shootS;
    [SerializeField] AudioSource audioS;
    float timePassed;
    float maxTime = 0.5f;

    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > maxTime)
        {

        }
        // Check if the left mouse button is held down or pressed
        if (Input.GetMouseButton(0))
        {
            // Check if the left mouse button was pressed (not held down)
            if (Input.GetMouseButtonDown(0))
            {
                Shoot(); // Shoot immediately when pressed
            }
            else
            {
                // For continuous firing, only shoot after a certain time interval
                ShootContinuous();
            }
        }
        else
        {
            // Reset continuous fire time when the mouse button is released
            continuousFireTime = 0f;
        }
    }
    

    public float CalculateFrameTime(int frame)
    {
        float frameTime = 1f;

        return frame * frameTime;
    }


    void ShootContinuous()
    {
        // Increment continuous fire time
        continuousFireTime += Time.deltaTime;

        // Check if enough time has passed for a continuous shot
        if (continuousFireTime >= 1f / continuousFireRate)
        {
            Shoot();
            continuousFireTime = 0f;
        }
    }

    void Shoot()
    {
        audioS.PlayOneShot(shootS);
        
        animator.Play("Aim");
        Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
        instantiatedProjectile.velocity = transform.forward * speed; // Use transform.forward for simplicity
    }
}
