using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
     GameObject player;
     ParticleSystem teleportElectric;
    ParticleSystem TeleportIDontEvenKnow;
    private void Start()
    {
        player = GameObject.Find("Player");
        teleportElectric = GameObject.Find("Teleport Explosion").GetComponent<ParticleSystem>();
        TeleportIDontEvenKnow = GameObject.Find("Teleport i dont even know").GetComponent<ParticleSystem>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 position = transform.position;
        if(position.y >= 1)
        {
            position.z -= 1f;
            position.y = -0.55f;
        }else if(position.y >= 1.5f)
        {
            position.y = -1.3f;
            position.z -= 1.3f;
        }
        else if (position.y >= 1.2f)
        {
            position.y = -1.2f;
            position.z -= 1.2f;
        }
        player.transform.position = position + Vector3.up;
        //teleportElectric.Play();
        TeleportIDontEvenKnow.Play();
    }
}
