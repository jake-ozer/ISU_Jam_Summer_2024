using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IInteractable
{
    public IngredientType type;

    public void Interact()
    {
        Debug.Log("You touched " + type.ingredientName);
        FindObjectOfType<IngredientManager>().UpdateIngredientHeld(type);
    }
}
