using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask interactLayer;

    private Camera cam;
    private GameObject currentHit;
    public bool hasInteracted = false;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            //highlight
            if (Physics.Raycast(ray, out hitInfo, distance))
            {
                if (hitObject.GetComponent<Outline>() != null && hitObject.GetComponent<IInteractable>() != null && 
                    !hasInteracted && hitObject.GetComponent<IInteractable>().canInteract)
                {
                    hitInfo.collider.gameObject.GetComponent<Outline>().enabled = true;
                    currentHit = hitInfo.collider.gameObject;
                }
            }

            //disable highlight on previous hit
            if (currentHit != null && hitObject.GetComponent<IInteractable>() == null)
            {
                currentHit.GetComponent<Outline>().enabled = false;
                currentHit = null;
                hasInteracted = false;
            }
            
            //click to interact
            if (Input.GetMouseButtonDown(0) && hitObject.GetComponent<IInteractable>() != null && !hasInteracted && hitObject.GetComponent<IInteractable>().canInteract)
            {
                hitInfo.collider.gameObject.GetComponent<Outline>().enabled = false;
                hitInfo.collider.gameObject.GetComponent<IInteractable>().Interact();
                hasInteracted = true;
            }
        }
    }
}
