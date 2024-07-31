using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBarrel : MonoBehaviour, IInteractable
{
    public bool canInteract => true;


    public void Interact()
    {
        if(FindObjectOfType<PickupManager>().curItem != null && FindObjectOfType<PickupManager>().curItem is PoisonBottle)
        {
            FindObjectOfType<PickupManager>().DropItem();
        }
    }
}
