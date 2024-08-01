using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBarrel : MonoBehaviour, IInteractable
{
    public bool canInteract => interactable;
    public bool interactable;

    public void Interact()
    {
        if(FindObjectOfType<PickupManager>().curItem != null && FindObjectOfType<PickupManager>().curItem is PoisonBottle)
        {
            FindObjectOfType<PickupManager>().DropItem();
        }
    }

    private void Update()
    {
        if (FindObjectOfType<PickupManager>().curItem is PoisonBottle)
        {
            interactable = true;
        }
        else
        {
            interactable = false;
        }
    }
}
