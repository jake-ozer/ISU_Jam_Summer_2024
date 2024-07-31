using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Poison Recipe", menuName = "Poison Recipe")]
public class PoisonRecipe : ScriptableObject
{
    public string poisonName;
    public string description;
    public List<IngredientType> ingredientList;
    public IngredientType effectIngredient; //Ingredient that represents desired effect
}
