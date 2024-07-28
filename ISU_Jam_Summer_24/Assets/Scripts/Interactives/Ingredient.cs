using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IInteractable, IPickup
{
    public IngredientType type;

    public bool canInteract => true;

    public void Interact()
    {
        //Debug.Log("You touched " + type.ingredientName);
        FindObjectOfType<PickupManager>().UpdateItemHeld(this);
    }
}
