using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OrderManager : MonoBehaviour
{
    public TMP_Text orderGiven;
    public List<Order> orderSequence;
    private int curOrderIndex = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            getOrder();
        }
    }

    public void getOrder()
    {
        orderGiven.text = orderSequence[curOrderIndex].orderContent;
        Debug.Log("Order is Given");
    }

    public void checkOrder()
    {
        List<IngredientType> potionEffectsIn = FindObjectOfType<Cauldron>().getFinalEffects();
        if (orderSequence[curOrderIndex].desiredEffects.All(i => potionEffectsIn.Contains(i)))
        {
            Debug.Log("Correct Poison");
            FindObjectOfType<PortalColor>().CorrectPoisonAnim();
            curOrderIndex++;

            //end game logic
            if(curOrderIndex >= orderSequence.Count) 
            {
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            Debug.Log("Something was off");
            FindObjectOfType<PortalColor>().IncorrectPoisonAnim();
        }
        FindObjectOfType<Cauldron>().resetEffects();
    }
}
