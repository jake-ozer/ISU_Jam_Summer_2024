using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cauldron : MonoBehaviour, IInteractable
{
    public GameObject poisonBottle;
    public Transform bottleSpawn;
    private List<IngredientType> ingredients;
    public bool interactable = true;
    public GameObject menuBack;
    public GameObject ingTextPrefab;
    private List<GameObject> uiDestroyList;

    public bool canInteract { get => interactable; }

    private void Start()
    {
        ingredients = new List<IngredientType>();
        uiDestroyList = new List<GameObject>();
    }

    public void Interact()
    {
        //if ingredient, add it to pot, otherwise start boiling
        if (FindObjectOfType<PickupManager>().curItem is Ingredient curIngredient)
        {
            ingredients.Add(curIngredient.type);
            var ingText = Instantiate(ingTextPrefab);
            ingText.GetComponent<TextMeshProUGUI>().text = curIngredient.type.ingredientName;
            ingText.transform.parent = menuBack.transform;
            uiDestroyList.Add(ingText);
            FindObjectOfType<PickupManager>().DropItem();
            //flash interactivity back to normal after a second
            Invoke("RestoreInteractivity", 0.5f);
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
        interactable = false;
        //check if ingredients in cauldron do make a poison
        PoisonRecipe curRecipe = FindObjectOfType<RecipeManager>().ValidateRecipe(ingredients);
        if (curRecipe != null)
        {
            Instantiate(poisonBottle, bottleSpawn);
            FindObjectOfType<PlayerInteract>().cauldronMenu.SetActive(false);
        }
        else
        {
            Debug.Log("You did not make a poison with a predefined recipe.");
            interactable = true;
        }
        EmptyCauldron();
    }

    private void EmptyCauldron()
    {
        ingredients.Clear();
        foreach(var go in uiDestroyList)
        {
            Destroy(go);
        }
    }
}
