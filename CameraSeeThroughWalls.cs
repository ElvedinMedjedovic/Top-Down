using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSeeThroughWalls : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SetActive(false);
    }
    //private void OnCollisionExit(Collision collision)
    //{
   //     wallRenderer.enabled = true;
   //     wallRenderer = null;
   // }
}
