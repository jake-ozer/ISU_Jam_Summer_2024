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
    public int indexNum; //Something that can be used to pull up the ingredient
    public bool isEffect; //tells wheather it is an actuall ingredient(false) or an effect(true)
    public int levelReq; //This tells how hard it is to make (+1 for each effect required to make it with 0 being a basic poison in input)
}
