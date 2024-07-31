using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, IInteractable
{
    public bool canInteract => interactable;
    private bool interactable = false;

    public void Interact()
    {
        PickupManager pm = FindObjectOfType<PickupManager>();
        if (pm.curItem is PoisonBottle)
        {
            //logic for submitting poison will go here
            FindObjectOfType<OrderManager>().checkOrder();
            pm.DropItem();
            FindObjectOfType<Cauldron>().interactable = true;
        }
    }

    void Update()
    {
        //only make it interactable when player holds poison
        if(FindObjectOfType<PickupManager>().curItem is PoisonBottle) 
        {
            interactable = true;
        }
        else
        {
            interactable = false;
        }
    }
}
