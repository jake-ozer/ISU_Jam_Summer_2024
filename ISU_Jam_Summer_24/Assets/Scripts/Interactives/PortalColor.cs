using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortalColor : MonoBehaviour
{

    [SerializeField] ParticleSystem redParticles = null;
    [SerializeField] ParticleSystem greenParticles = null;
    public GameObject portalText;


    // Start is called before the first frame update
    void Start()
    {
        portalText.SetActive(false);
    }

    public void CorrectPoisonAnim()
    {
        greenParticles.Play();
        StartCoroutine(ShowPortalText(true));
    }

    public void IncorrectPoisonAnim()
    {
        redParticles.Play();
        StartCoroutine(ShowPortalText(false));
    }

    private IEnumerator ShowPortalText(bool result)
    {
        portalText.SetActive(true);
        if (result)
        {
            portalText.GetComponent<TextMeshProUGUI>().text = "Recieved Favorably.<br>Go to mailbox for a new order.";
        }
        else
        {
            portalText.GetComponent<TextMeshProUGUI>().text = "Recieved Poorly.<br>Try again with a better poison.";
        }
        yield return new WaitForSeconds(7f);
        portalText.SetActive(false);
    }

}
