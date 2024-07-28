using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public List<IngredientType> pageContents;
    //UI References
    public GameObject pageOneParent;
    public GameObject pageTwoParent;

    private void Start()
    {
        //load initial two pages
        pageOneParent.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void PageRight()
    {

    }

    public void PageLeft() 
    {
        
    }

}
