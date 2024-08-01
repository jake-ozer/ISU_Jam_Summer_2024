using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public TextMeshProUGUI notifText;
    public IPickup curItem;
    public AudioClip ingredientPickupSound;
    public AudioSource cameraAudioSource;

    //ONLY UPDATES ITEM if not currently holding one
    public void UpdateItemHeld(IPickup item)
    {
        if (curItem == null)
        {
            curItem = item;
            if (curItem is Ingredient ingredient)
            {
                notifText.gameObject.SetActive(true);
                notifText.text = "You are holding " + ingredient.type.ingredientName + ". Press f to drop";
                cameraAudioSource.PlayOneShot(ingredientPickupSound);
            }
        }
    }

    public void DropItem()
    {
        if (curItem is Ingredient) //destory ingredient
        {
            notifText.gameObject.SetActive(false);
        }
        else if (curItem != null) //destroy poison bottle
        {
            MonoBehaviour mb = curItem as MonoBehaviour;
            Destroy(mb.gameObject);
        }
        curItem = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            DropItem();
        }
    }
}
