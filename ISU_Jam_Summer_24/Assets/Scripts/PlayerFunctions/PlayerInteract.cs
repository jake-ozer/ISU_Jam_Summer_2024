using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask interactLayer;

    private Camera cam;
    private GameObject currentHit;
    public bool hasInteracted = false;
    public GameObject cauldronMenu;
    public GameObject ingredientLabel;
    public GameObject poisonMenu;

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
                    //enable cauldron menu
                    if(hitObject.GetComponent<Cauldron>())
                    {
                        cauldronMenu.SetActive(true);
                    }
                    else
                    {
                        cauldronMenu.SetActive(false);
                    }

                    //enable ingredient label
                    if (hitObject.GetComponent<Ingredient>())
                    {
                        ingredientLabel.GetComponent<TextMeshProUGUI>().text = hitObject.GetComponent<Ingredient>().type.ingredientName;
                        ingredientLabel.SetActive(true);
                    }
                    else
                    {
                        ingredientLabel.SetActive(false);
                    }

                    //enable poison menu
                    if (hitObject.GetComponent<PoisonBottle>())
                    {
                        poisonMenu.SetActive(true);
                    }
                    else
                    {
                        poisonMenu.SetActive(false);
                    }
                }
            }

            //disable highlight on previous hit
            if (currentHit != null && hitObject.GetComponent<IInteractable>() == null)
            {
                currentHit.GetComponent<Outline>().enabled = false;
                currentHit = null;
                hasInteracted = false;
                cauldronMenu.SetActive(false);
                ingredientLabel.SetActive(false);
                poisonMenu.SetActive(false);
            }
            
            //click to interact
            if (Input.GetMouseButtonDown(0) && hitObject.GetComponent<IInteractable>() != null && !hasInteracted && hitObject.GetComponent<IInteractable>().canInteract)
            {
                hitInfo.collider.gameObject.GetComponent<Outline>().enabled = false;
                hitInfo.collider.gameObject.GetComponent<IInteractable>().Interact();
                hasInteracted = true;
                ingredientLabel.SetActive(false);
            }
        }
    }
}
