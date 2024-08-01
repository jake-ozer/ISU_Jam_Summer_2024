using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderLetter : MonoBehaviour
{
    public GameObject background;

    public void EnableLetter()
    {
        background.SetActive(true);
    }

    public void DisableLetter()
    {
        background.SetActive(false);
    }
}
