using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    bool isAttacking;
    bool readyToAttack = true;
    [SerializeField] GameObject Weapon;
    [SerializeField] float attackCooldown;
    Animator anim;
    
    public int weaponDamage;

    private void Start()
    {
        anim = Weapon.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(readyToAttack)
            {
                Attack();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with enemy detected");
        if (other.CompareTag("Enemy") && isAttacking)
        {
            Debug.Log("Hit Enemy");
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.enemyHp -= weaponDamage;
            }
        }
    }
    IEnumerator AttackCooldown()
    {
        StartCoroutine(BoolReset());
        yield return new WaitForSeconds(attackCooldown);
        readyToAttack = true;
        
    }
    IEnumerator BoolReset()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Attack", false);
        isAttacking = false;
    }
    public void Attack()
    {
        if (readyToAttack)
        {
            isAttacking = true;
            readyToAttack = false;
            anim.SetBool("Attack", true);

            // Apply damage to enemies within the weapon's collider
            StartCoroutine(AttackCooldown());
        }
    }


}
