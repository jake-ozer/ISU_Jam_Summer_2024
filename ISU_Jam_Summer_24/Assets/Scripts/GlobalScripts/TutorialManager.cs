using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutMenu;
    public TextMeshProUGUI tutText;
    public List<string> tutMessages;
    public GameObject playerObject;
    public float delayTime = 1.75f;
    public int mesIndex = 0;

    private void Start()
    {
        //start first tutorial message after a couple seconds when starting game
        Invoke("EnablePopup", 1f);
    }

    public void EnablePopup()
    {
        Invoke("EnablePopupHelper", delayTime);
    }

    private void EnablePopupHelper()
    {
        tutText.text = tutMessages[mesIndex];
        tutMenu.SetActive(true);
        Camera.main.GetComponent<PlayerCam>().enabled = false;
        playerObject.GetComponent<PlayerMove>().enabled = false;
        playerObject.GetComponent<PlayerInteract>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        mesIndex++;
    }

    //used by exit button on popup menu
    public void DisablePopup()
    {
        Camera.main.GetComponent<PlayerCam>().enabled = true;
        playerObject.GetComponent<PlayerMove>().enabled = true;
        playerObject.GetComponent<PlayerInteract>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        tutMenu.SetActive(false);
    }
}
