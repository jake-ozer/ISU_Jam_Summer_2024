using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : Interactable
{
    [SerializeField] private GameObject canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public override void Interact()
    {
        Debug.Log("you interacted with a letter");
        canvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; 
    }
}
