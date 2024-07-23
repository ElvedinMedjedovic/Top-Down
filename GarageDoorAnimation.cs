using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoorAnimation : MonoBehaviour
{

    [SerializeField] Animator gDoorAnim;
    [SerializeField] Animator vaultDoorAnim;
    [SerializeField] GameObject robotBoss;
    [SerializeField] GameObject bossHp;
    [SerializeField] AudioSource bossAudio;
    [SerializeField] AudioSource regularAudio;
    [SerializeField] AudioSource gDoorSource;
    [SerializeField] AudioClip gDoorOpening;



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("AnimTriggerGdoor"))
        {
            gDoorAnim.SetTrigger("hasPassed");
            gDoorSource.PlayOneShot(gDoorOpening);
        }
        if(other.gameObject.CompareTag("AnimTriggerFall"))
        {
            gDoorAnim.SetTrigger("Falls");
        }
        if (other.gameObject.CompareTag("AnimTriggerVault"))
        {
            vaultDoorAnim.SetBool ("Trigger", true);
            robotBoss.SetActive(true);
            StartCoroutine(WaitForDoor());

        }
    }
    IEnumerator WaitForDoor()
    {
        yield return new WaitForSeconds(3);
        transform.position = new Vector3(136, 0.83f, 464);
        vaultDoorAnim.SetBool("Trigger", false);
        bossHp.SetActive(true);
        bossAudio.enabled = true;
        regularAudio.enabled = false;
    }
}
