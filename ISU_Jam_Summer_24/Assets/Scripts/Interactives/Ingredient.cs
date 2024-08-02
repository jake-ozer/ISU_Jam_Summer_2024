using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IInteractable, IPickup
{
    public IngredientType type;

    public bool canInteract => interactable;
    private bool interactable = false;

    public void Interact()
    {
        FindObjectOfType<PickupManager>().UpdateItemHeld(this);
    }

    private void Update()
    {
        if (FindObjectOfType<PickupManager>().curItem != null)
        {
            interactable = false;
        }
        else
        {
            interactable = true;
        }
    }
}
