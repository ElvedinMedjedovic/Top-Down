using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFollow : MonoBehaviour
{
    Rigidbody rocketRigidbody;
    PlayerController playerController;
    GameObject player;
    [SerializeField] GameObject particle;
    [SerializeField] AudioSource audioSource; 
    [SerializeField] AudioClip rocketBoost;
    float speed = 100;
    public float damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("FollowR");
        StartCoroutine(WaitForAudioEnd());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        rocketRigidbody.AddForce(lookDirection * speed);
        if(transform.position.y < 0.3f)
        {
            rocketRigidbody.AddForce(Vector3.up * 20);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.health -= damage;
        }
        particle.SetActive(true);
        speed = 0;
        rocketRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        rocketRigidbody.constraints = RigidbodyConstraints.FreezePositionX;
        rocketRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        StartCoroutine(WaitForParticle());
        Destroy(gameObject, 1);
    }
    IEnumerator WaitForParticle()
    {
        yield return new WaitForSeconds(.45f);
        gameObject.SetActive(false);
    }
    IEnumerator WaitForAudioEnd()
    {
        yield return new WaitForSeconds(2f);
        audioSource.PlayOneShot(rocketBoost);

    }
}
