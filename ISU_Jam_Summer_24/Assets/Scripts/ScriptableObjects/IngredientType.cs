using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredient")]
public class IngredientType : ScriptableObject
{
    public string ingredientName;
    public string description;
    public Sprite img;
}
