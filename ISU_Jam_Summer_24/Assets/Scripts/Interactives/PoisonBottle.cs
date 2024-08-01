using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBottle : MonoBehaviour, IInteractable, IPickup
{
    private Transform playerHoldSpot;

    public bool canInteract => true;

    private void Awake()
    {
        playerHoldSpot = Camera.main.transform.GetChild(0);
    }

    public void Interact()
    {
        //have player hold poison bottle
        transform.position = playerHoldSpot.position;
        transform.parent = playerHoldSpot;
        GetComponent<Animator>().enabled = false;
        FindObjectOfType<PickupManager>().UpdateItemHeld(this);
        FindObjectOfType<Cauldron>().ResetPoisonMenu();
    }
}
