using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBook : MonoBehaviour, IInteractable
{
    public bool canInteract => true;
    public Transform bookViewCamSpot;
    public GameObject playerObject;
    public GameObject crosshair;
    private Vector3 startPos;
    private Quaternion startRot;
    private bool inBookView = false;

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
        if (inBookView && Input.GetKeyDown(KeyCode.Space))
        {
            ExitBookView();
            inBookView = false;
        }
    }

    private void EnterBookView()
    {
        inBookView = true;
        Camera.main.GetComponent<PlayerCam>().enabled = false;
        playerObject.GetComponent<PlayerMove>().enabled = false;
        crosshair.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        startPos = Camera.main.transform.position;
        startRot = Camera.main.transform.rotation;
        StartCoroutine("LerpToPos");
    }

    private void ExitBookView()
    {
        StartCoroutine("LerpToStart");
        Camera.main.GetComponent<PlayerCam>().enabled = true;
        playerObject.GetComponent<PlayerMove>().enabled = true;
        crosshair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
    }

    private IEnumerator LerpToStart()
    {
        lerpFactor = 0f;

        while (lerpFactor < 1f)
        {
            lerpFactor += Time.deltaTime * lerpSpeed;
            Camera.main.transform.position = Vector3.Lerp(bookViewCamSpot.position, startPos, lerpFactor);
            Camera.main.transform.rotation = Quaternion.Lerp(bookViewCamSpot.rotation, startRot, lerpFactor);
            yield return null;
        }
    }
}
