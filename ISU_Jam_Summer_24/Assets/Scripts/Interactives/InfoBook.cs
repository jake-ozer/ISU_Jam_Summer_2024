using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBook : MonoBehaviour, IInteractable
{
    public bool canInteract => interactable;
    public Transform bookViewCamSpot;
    public GameObject playerObject;
    public GameObject crosshair;
    public GameObject bookControls;
    private Vector3 startPos;
    private Quaternion startRot;
    private bool inBookView = false;
    private bool interactable = false;

    //lerping movement
    public float lerpSpeed;
    private float lerpFactor;

    public void Interact()
    {
        EnterBookView();
    }

    private void Update()
    {
        //exit book view
        /*if (inBookView && Input.GetKeyDown(KeyCode.Space))
        {
            ExitBookView();
            inBookView = false;
        }*/

        if(FindObjectOfType<PickupManager>().curItem != null)
        {
            interactable = false;
        }
        else
        {
            interactable = true;
        }
    }

    private void EnterBookView()
    {
        inBookView = true;
        Camera.main.GetComponent<PlayerCam>().enabled = false;
        playerObject.GetComponent<PlayerMove>().enabled = false;
        crosshair.SetActive(false);

        startPos = Camera.main.transform.position;
        startRot = Camera.main.transform.rotation;
        StartCoroutine("LerpToPos");
    }

    public void ExitBookView()
    {
        StartCoroutine("LerpToStart");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inBookView = false;
    }

    private IEnumerator LerpToPos()
    {
        lerpFactor = 0f;

        while (lerpFactor < 1f)
        {
            lerpFactor += Time.deltaTime * lerpSpeed;
            Camera.main.transform.position = Vector3.Lerp(startPos, bookViewCamSpot.position, lerpFactor);
            Camera.main.transform.rotation = Quaternion.Lerp(startRot, bookViewCamSpot.rotation, lerpFactor);
            yield return null;
        }
        bookControls.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator LerpToStart()
    {
        bookControls.SetActive(false);

        lerpFactor = 0f;

        while (lerpFactor < 1f)
        {
            lerpFactor += Time.deltaTime * lerpSpeed;
            Camera.main.transform.position = Vector3.Lerp(bookViewCamSpot.position, startPos, lerpFactor);
            Camera.main.transform.rotation = Quaternion.Lerp(bookViewCamSpot.rotation, startRot, lerpFactor);
            yield return null;
        }

        Camera.main.GetComponent<PlayerCam>().enabled = true;
        playerObject.GetComponent<PlayerMove>().enabled = true;
        crosshair.SetActive(true);
    }
}
