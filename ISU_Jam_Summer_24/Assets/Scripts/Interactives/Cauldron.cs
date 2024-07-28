using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour, IInteractable
{
    public GameObject poisonBottle;
    public Transform bottleSpawn;
    private List<IngredientType> ingredients;

    public bool canInteract { get => true; }

    private void Start()
    {
        ingredients = new List<IngredientType>();
    }

    public void Interact()
    {
        //flash interactivity back to normal after a second
        Invoke("RestoreInteractivity", 0.78f);

        //if ingredient, add it to pot, otherwise start boiling
        if (FindObjectOfType<PickupManager>().curItem is Ingredient curIngredient)
        {
            ingredients.Add(curIngredient.type);
            FindObjectOfType<PickupManager>().DropItem();
        }
        else
        {
            BoilCauldron();
        }
    }

    private void RestoreInteractivity()
    {
        FindObjectOfType<PlayerInteract>().hasInteracted = false;
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
