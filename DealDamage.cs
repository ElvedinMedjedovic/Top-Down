using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public float damage;
    RectTransform bossHp;
    Enemy enemy;
    RobotBoss robotBoss;
    PlayerController playerController;
    [SerializeField] bool isEnemy;
    public static float width = 792;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && isEnemy == false)
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.enemyHp -= damage;
            Debug.Log("hit" + enemy.enemyHp);

        }else if(collision.gameObject.CompareTag("Player") && isEnemy)
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.health -= damage;
        }else if (collision.gameObject.CompareTag("Boss") && isEnemy == false)
        {
            width -= 1.98f;
            robotBoss = collision.gameObject.GetComponent<RobotBoss>();
            bossHp.sizeDelta = new Vector2(width, 22);

        }
        Destroy(gameObject);
    }
    private void Start()
    {
        //width = 792;
        bossHp = GameObject.Find("BossHp").GetComponent<RectTransform>();
        Destroy(gameObject, 1);
    }
    private void Update()
    {
        
    }
}
