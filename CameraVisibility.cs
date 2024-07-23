using UnityEngine;

public class CameraVisibility : MonoBehaviour
{
    public float visibilityRange = 16f; // Adjust the range as needed
    public string excludedTag = "NoVisibility"; // Tag to exclude from visibility toggling

    void Update()
    {
        // Cast a ray from the camera to check for collisions with objects
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, visibilityRange);

        // Toggle visibility based on whether the camera is colliding with the object
        foreach (RaycastHit hit in hits)
        {
            MeshRenderer meshRenderer = hit.collider.GetComponent<MeshRenderer>();

            if (meshRenderer != null && !IsExcluded(hit.collider.gameObject))
            {
                meshRenderer.enabled = false; // Set to false when colliding
            }
        }

        // Enable visibility for all MeshRenderers not currently colliding with the camera and not excluded
        foreach (MeshRenderer meshRenderer in FindObjectsOfType<MeshRenderer>())
        {
            if (!IsMeshRendererHit(meshRenderer, hits) && !IsExcluded(meshRenderer.gameObject))
            {
                meshRenderer.enabled = true;
            }
        }
    }

    bool IsMeshRendererHit(MeshRenderer meshRenderer, RaycastHit[] hits)
    {
        foreach (RaycastHit hit in hits)
        {
            if (meshRenderer == hit.collider.GetComponent<MeshRenderer>())
            {
                return true;
            }
        }
        return false;
    }

    bool IsExcluded(GameObject obj)
    {
        return obj.CompareTag(excludedTag);
    }
}
