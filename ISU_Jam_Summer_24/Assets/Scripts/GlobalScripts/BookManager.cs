using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    public List<IngredientType> pageContents;
    //UI References
    //page one
    public TextMeshProUGUI pageOneTitle;
    public TextMeshProUGUI pageOneText;
    public TextMeshProUGUI pageOneNumber;
    public Image pageOneImg;
    //page two
    public TextMeshProUGUI pageTwoTitle;
    public TextMeshProUGUI pageTwoText;
    public TextMeshProUGUI pageTwoNumber;
    public Image pageTwoImg;

    //page trackers
    private int curPairIndex = 0;

    //sound
    public AudioClip pageTurnSound;

    private void Update()
    {
        //page one
        pageOneTitle.text = pageContents[curPairIndex].ingredientName;
        pageOneText.text = pageContents[curPairIndex].description;
        int pageOneNum = curPairIndex + 1;
        pageOneNumber.text = (pageOneNum).ToString();
        pageOneImg.sprite = pageContents[curPairIndex].img;
        //page two
        pageTwoTitle.text = pageContents[curPairIndex+1].ingredientName;
        pageTwoText.text = pageContents[curPairIndex+1].description;
        int pageTwoNum = curPairIndex + 2;
        pageTwoNumber.text = (pageTwoNum).ToString();
        pageTwoImg.sprite = pageContents[curPairIndex+1].img;
    }

    public void PageRight()
    {
        if(curPairIndex < pageContents.Count - 2)
        {
            curPairIndex += 2;
            Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(pageTurnSound);
        }
    }

    public void PageLeft() 
    {
        if(curPairIndex >= 2)
        {
            curPairIndex -= 2;
            Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(pageTurnSound);
        }
    }

}
