using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour, IInteractable
{
    public GameObject poisonBottle;
    public Transform bottleSpawn;
    private List<IngredientType> ingredients;
    private List<IngredientType> brewParts; //This list stores what the cauldron current has in it for adding future stuff

    public bool canInteract { get => true; }

    private void Start()
    {
        ingredients = new List<IngredientType>();
        brewParts = new List<IngredientType>();
    }

    public void Interact()
    {
        //flash interactivity back to normal after a second
        Invoke("RestoreInteractivity", 0.78f);

        //if ingredient, add it to pot, otherwise start boiling
        if (FindObjectOfType<PickupManager>().curItem is Ingredient curIngredient)
        {
            ingredients.Add(curIngredient.type);
            updateBrewEffects(curIngredient.type);
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

    //checks to see what should be combined and then repaces them out with the output effect
    private void updateBrewEffects(IngredientType input)
    {
        RecipeManager recipeMan = FindObjectOfType<RecipeManager>();
        int i = 0;
        bool addTo = true;
        while(i<brewParts.Count && addTo)
        {
            if(recipeMan.getMatchable(input,brewParts[i]))
            {
                addTo = false;
                IngredientType otherPart = brewParts[i];
                brewParts.Remove(otherPart);
                brewParts.Add(recipeMan.returnEffectItem(new List<IngredientType>{input,otherPart}));
            }
            i+=1;
        }
        if(addTo)
        {
            brewParts.Add(input);
        }
        Debug.Log("You added " + brewParts[brewParts.Count-1].ingredientName);
    }
}
