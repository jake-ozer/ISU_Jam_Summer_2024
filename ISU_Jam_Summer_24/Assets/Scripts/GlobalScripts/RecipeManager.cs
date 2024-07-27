using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public List<PoisonRecipe> validRecipes;

    private void Awake()
    {
        //validRecipes = new List<PoisonRecipe>();
    }

    public PoisonRecipe ValidateRecipe(List<IngredientType> ingredients)
    {
        //for each valid recipe
        foreach (PoisonRecipe recipe in validRecipes)
        {
            //check if ingredients param matches up with one of them, any order
            if (recipe.ingredientList.All(i => ingredients.Contains(i)) && recipe.ingredientList.Count == ingredients.Count)
            {
                Debug.Log("Found poison... " + recipe.poisonName);
                return recipe;
            }
        }
        return null;
    }
}
