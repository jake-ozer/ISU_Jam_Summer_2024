using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mailbox : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI orderText;
    public GameObject orderLetter;
    public GameObject playerObject;

    public bool canInteract => true;

    public void Interact()
    {
        FindObjectOfType<OrderManager>().getOrder();
        EnableOrderLetter();
    }

    public void EnableOrderLetter()
    {
        orderLetter.SetActive(true);
        Camera.main.GetComponent<PlayerCam>().enabled = false;
        playerObject.GetComponent<PlayerMove>().enabled = false;
        playerObject.GetComponent<PlayerInteract>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DisableOrderLetter()
    {
        orderLetter.SetActive(false);
        Camera.main.GetComponent<PlayerCam>().enabled = true;
        playerObject.GetComponent<PlayerMove>().enabled = true;
        playerObject.GetComponent<PlayerInteract>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
