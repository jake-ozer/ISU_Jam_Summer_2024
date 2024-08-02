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
    //poison menu UI
    public GameObject poisonMenuBack;
    private List<GameObject> uiPoisonDestroyList;

    public bool canInteract { get => interactable; }
    public bool interactable = true;

    //sound
    public AudioClip ingredientDropSound;
    public AudioClip boilSound;

    //cauldron constraint patch
    public TextMeshProUGUI cauldronMenuTitleText;
    private int ingCount = 0;

    private void Start()
    {
        ingredients = new List<IngredientType>();
        brewParts = new List<IngredientType>();
        uiDestroyList = new List<GameObject>();
        uiPoisonDestroyList = new List<GameObject>();
    }

    public void Interact()
    {
        //flash interactivity back to normal after a second
        Invoke("RestoreInteractivity", 0.78f);

        //if ingredient, add it to pot, otherwise start boiling
        if (FindObjectOfType<PickupManager>().curItem is Ingredient curIngredient)
        {
            ingCount++;
            GetComponent<AudioSource>().PlayOneShot(ingredientDropSound);
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

    private void Update()
    {
        //constrain to two
        if(cauldronMenuTitleText!= null)
        {
            cauldronMenuTitleText.text = "Cauldron Contents: " + ingCount + "/2";
        }
    }

    private void RestoreInteractivity()
    {
        FindObjectOfType<PlayerInteract>().hasInteracted = false;
    }

    private void BoilCauldron()
    {
        //make potion of anything
        if(ingredients.Count!=0)
        {
            //tutorial message
            var tutMan = FindObjectOfType<TutorialManager>();
            if (tutMan.mesIndex == 3)
            {
                tutMan.EnablePopup();
            }
            ingCount = 0;

            GetComponent<AudioSource>().PlayOneShot(boilSound);
            Instantiate(poisonBottle, bottleSpawn);
            finalEffects = FindObjectOfType<RecipeManager>().getPoisonEffects(brewParts);
            //update poison menu with final effects
            if(finalEffects.Count == 0)
            {
                var poisonText = Instantiate(ingTextPrefab);
                poisonText.GetComponent<TextMeshProUGUI>().text = "No effects";
                poisonText.transform.parent = poisonMenuBack.transform;
                uiPoisonDestroyList.Add(poisonText);
            }
            else
            {
                foreach (var effect in finalEffects)
                {
                    var poisonText = Instantiate(ingTextPrefab);
                    poisonText.GetComponent<TextMeshProUGUI>().text = effect.ingredientName;
                    poisonText.transform.parent = poisonMenuBack.transform;
                    uiPoisonDestroyList.Add(poisonText);
                }
            }    
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

    public void ResetPoisonMenu()
    {
        //to fix a slight timing bug
        Invoke("RPM_Helper", 0.8f);
    }

    private void RPM_Helper()
    {
        foreach (var go in uiPoisonDestroyList)
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
