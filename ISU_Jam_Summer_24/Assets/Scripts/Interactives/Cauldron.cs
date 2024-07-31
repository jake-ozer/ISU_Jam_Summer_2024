using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cauldron : MonoBehaviour, IInteractable
{
    public GameObject poisonBottle;
    public Transform bottleSpawn;
    private List<IngredientType> ingredients;
    private List<IngredientType> brewParts; //This list stores what the cauldron current has in it for adding future stuff
    private List<IngredientType> finalEffects;

    //cauldron UI elements
    public GameObject menuBack;
    public GameObject ingTextPrefab;
    private List<GameObject> uiDestroyList;


    public bool canInteract { get => interactable; }
    public bool interactable = true;

    private void Start()
    {
        ingredients = new List<IngredientType>();
        brewParts = new List<IngredientType>();
        uiDestroyList = new List<GameObject>();
    }

    public void Interact()
    {
        //flash interactivity back to normal after a second
        Invoke("RestoreInteractivity", 0.78f);

        //if ingredient, add it to pot, otherwise start boiling
        if (FindObjectOfType<PickupManager>().curItem is Ingredient curIngredient)
        {
            ingredients.Add(curIngredient.type);
            ingredients.Add(curIngredient.type);
            var ingText = Instantiate(ingTextPrefab);
            ingText.GetComponent<TextMeshProUGUI>().text = curIngredient.type.ingredientName;
            ingText.transform.parent = menuBack.transform;
            uiDestroyList.Add(ingText);
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
        /*PoisonRecipe curRecipe = FindObjectOfType<RecipeManager>().ValidateRecipe(ingredients);
        if (curRecipe != null)
        {
            Instantiate(poisonBottle, bottleSpawn);
        }
        else
        {
            Debug.Log("You did not make a poison with a predefined recipe.");
        }*/
        //Now will make potion of anything
        if(ingredients.Count!=0)
        {
            Instantiate(poisonBottle, bottleSpawn);
            finalEffects = FindObjectOfType<RecipeManager>().getPoisonEffects(brewParts);
        }
        EmptyCauldron();
    }

    private void EmptyCauldron()
    {
        ingredients.Clear();
        brewParts.Clear();
        foreach (var go in uiDestroyList)
        {
            Destroy(go);
        }

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
        //This half checks again for more complex combinations
        if(!addTo)
        {
            bool stoppedCombining = false;
            IngredientType lastCombo = brewParts[brewParts.Count-1];
            while(!stoppedCombining)
            {
                if(recipeMan.getMatchList(lastCombo.indexNum).Count!=0)
                {
                    i=0;
                    bool unFin = true;
                    while(i<brewParts.Count && unFin)
                    {
                        if(recipeMan.getMatchable(lastCombo,brewParts[i]))
                        {   
                            unFin = false;
                            IngredientType otherPart = brewParts[i];
                            brewParts.Remove(otherPart);
                            brewParts.Remove(lastCombo);
                            brewParts.Add(recipeMan.returnEffectItem(new List<IngredientType>{lastCombo,otherPart}));
                            lastCombo = brewParts[brewParts.Count-1];
                        }
                        i+=1;
                    }

                    if(unFin)
                    {
                        stoppedCombining = true;
                    }
                }
                else
                {
                    stoppedCombining = true;
                }
            }
        }

        //If it made it through just add the ingredient
        else
        {
            brewParts.Add(input);
        }
        Debug.Log("You added " + brewParts[brewParts.Count-1].ingredientName);
    }

    public List<IngredientType> getFinalEffects()
    {
        return this.finalEffects;
    }

    public void resetEffects()
    {
        finalEffects = new List<IngredientType>{};
    }
}
