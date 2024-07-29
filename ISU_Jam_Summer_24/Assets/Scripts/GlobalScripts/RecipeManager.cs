using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public List<IngredientType> validIngredients;
    public List<PoisonRecipe> validRecipes;
    public List<List<IngredientType>> IngredientMatches;
    public List<int> testNums;

    public void start()
    {
        
    }
    private void Awake()
    {
        //validRecipes = new List<PoisonRecipe>();
        setUpMatches();
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

    //at the start of the game for each ingredient make a list of what it combines with to make for faster searching in the future
    public void setUpMatches()
    {
        int num = 0;
        IngredientMatches = new List<List<IngredientType>>{};
        testNums.Add(-1);
        foreach(IngredientType ingredientIn in validIngredients)
        {
            List<IngredientType> ingredientMatch = new List<IngredientType>{};
            ingredientIn.indexNum = num;
            testNums.Add(ingredientIn.indexNum);
            foreach(PoisonRecipe recipieCheck in validRecipes)
            {
                if(recipieCheck.ingredientList[0] == ingredientIn)
                {
                    ingredientMatch.Add(recipieCheck.ingredientList[1]);
                }
                if(recipieCheck.ingredientList[1] == ingredientIn)
                {
                    ingredientMatch.Add(recipieCheck.ingredientList[0]);
                }
            }
            num+=1;
            IngredientMatches.Add(ingredientMatch);
        }
    }

    //tells if there if the two will have an effect
    public bool getMatchable(IngredientType ingredient1, IngredientType ingredient2)
    {
        if(IngredientMatches[ingredient1.indexNum].Contains(ingredient2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //reuse of Validate recipie except it gives the new effect ingredient instead
    public IngredientType returnEffectItem(List<IngredientType> ingredients)
    {
        //for each valid recipe
        foreach (PoisonRecipe recipe in validRecipes)
        {
            //check if ingredients param matches up with one of them, any order
            if (recipe.ingredientList.All(i => ingredients.Contains(i)) && recipe.ingredientList.Count == ingredients.Count)
            {
                Debug.Log("Found poison... " + recipe.poisonName);
                return recipe.effectIngredient;
            }
        }
        return null;
    }

    //Gives what the potion actually does
    public List<IngredientType> getPoisonEffects(List<IngredientType> poisonBrew)
    {
        List<IngredientType> totalEffects = new List<IngredientType>{};
        foreach(IngredientType effect in poisonBrew)
        {
            if(effect.isEffect)
            {
                totalEffects.Add(effect);
            }
        }
        string testmessage = "A ";
        foreach(IngredientType effect in totalEffects)
        {
            testmessage += effect.ingredientName + " ";
        }
        testmessage += "Poison";
        Debug.Log(testmessage);
        return totalEffects;
    }

    public List<IngredientType> getMatchList(int indexNum)
    {
        return IngredientMatches[indexNum];
    }
}
