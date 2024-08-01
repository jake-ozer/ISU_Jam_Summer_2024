using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Order")]
public class Order : ScriptableObject
{
    public List<IngredientType> desiredEffects;
    public string orderContent;
}
