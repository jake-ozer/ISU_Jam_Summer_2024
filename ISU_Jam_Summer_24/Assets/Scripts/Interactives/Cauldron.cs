using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour, IInteractable
{
    List<IngredientType> ingredients;

    private void Start()
    {
        ingredients = new List<IngredientType>();
    }

    public void Interact()
    {
        Debug.Log("You touched cauldron");
        var curIngredient = FindObjectOfType<IngredientManager>().curIngredient;
        if (curIngredient != null)
        {
            ingredients.Add(curIngredient);
            FindObjectOfType<IngredientManager>().DropIngredient();
        }
    }
}
