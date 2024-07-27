using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour, IInteractable
{
    public GameObject poisonBottle;
    public Transform bottleSpawn;
    private List<IngredientType> ingredients;

    private void Start()
    {
        ingredients = new List<IngredientType>();
    }

    public void Interact()
    {
        //Debug.Log("You touched cauldron");
        var curIngredient = FindObjectOfType<IngredientManager>().curIngredient;
        if (curIngredient != null)
        {
            ingredients.Add(curIngredient);
            FindObjectOfType<IngredientManager>().DropIngredient();
        }
        else
        {
            BoilCauldron();
        }
    }

    private void BoilCauldron()
    {
        //check if ingredients in cauldron do make a poison
        PoisonRecipe curRecipe = FindObjectOfType<RecipeManager>().ValidateRecipe(ingredients);
        if (curRecipe != null)
        {
            Instantiate(poisonBottle, bottleSpawn);
        }
        else
        {
            Debug.Log("You did not make a poison with a predefined recipe.");
        }
        EmptyCauldron();
    }

    private void EmptyCauldron()
    {
        ingredients.Clear();
    }
}
