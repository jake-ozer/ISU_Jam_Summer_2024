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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            redParticles.Play();
            ShowPortalText(false);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            greenParticles.Play();
            ShowPortalText(true);
        }
    }

    private IEnumerator ShowPortalText(bool result)
    {
        
        portalText.SetActive(true);
        if (result)
        {
            portalText.GetComponent<TextMeshProUGUI>().text = "Recieved Favorably";
        }
        else
        {
            portalText.GetComponent<TextMeshProUGUI>().text = "Recieved Poorly";
        }
        yield return new WaitForSeconds(3);
        portalText.SetActive(false);
    }

}
