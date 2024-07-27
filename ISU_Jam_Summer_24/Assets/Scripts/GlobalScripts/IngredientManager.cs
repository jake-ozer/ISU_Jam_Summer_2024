using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public TextMeshProUGUI notifText;
    public IngredientType curIngredient;

    public void UpdateIngredientHeld(IngredientType type)
    {
        curIngredient = type;
        notifText.gameObject.SetActive(true);
        notifText.text = "You are holding " + curIngredient.ingredientName+". Press f to drop";
    }

    public void DropIngredient()
    {
        notifText.gameObject.SetActive(false);
        curIngredient = null;
    }

    private void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            DropIngredient();
        }
    }
}
