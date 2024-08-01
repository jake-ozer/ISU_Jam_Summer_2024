using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    //public List<IngredientType> effects;
    //public int level;
    //public List<IngredientType> order;
    public TMP_Text orderGiven;
    public List<Order> orderSequence;
    private int curOrderIndex = 0;

    // Start is called before the first frame update
    void Awake()
    {
/*        order = new List<IngredientType>{};
        List<IngredientType> totalIng = FindObjectOfType<RecipeManager>().getIngredients();
        effects = new List<IngredientType>{};
        foreach(IngredientType effect in totalIng)
        {
            if(effect.isEffect)
            {
                effects.Add(effect);
                effect.levelReq+=2;
            }
        }
        level = 2;*/
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            getOrder();
        }
    }

    public void getOrder()
    {
        orderGiven.text = orderSequence[curOrderIndex].orderContent.Replace("\n", "\n");

       /* if(order.Count == 0)
        {
            int maxLoops = 100;
            int loop=0;
            order = new List<IngredientType>{};
            List<int> takenEffects = new List<int>{};
            int inputEffect = Random.Range(0,effects.Count);
            int orderLevel = 0;
            while(orderLevel+effects[inputEffect].levelReq > level && loop < maxLoops)
            {
                inputEffect = Random.Range(0,effects.Count);
                loop+=1;
            }
            order.Add(effects[inputEffect]);
            takenEffects.Add(inputEffect);
            inputEffect = Random.Range(0,effects.Count);
            orderLevel+=effects[inputEffect].levelReq;

            int loop2 = 0;
            while(level - orderLevel >= 2 && loop2<maxLoops)
            {
                inputEffect = Random.Range(0,effects.Count);
                loop=0;
                while(orderLevel+effects[inputEffect].levelReq > level || takenEffects.Contains(inputEffect) && loop<maxLoops)
                {
                    inputEffect = Random.Range(0,effects.Count);
                    loop+=1;
                }
                order.Add(effects[inputEffect]);
                takenEffects.Add(inputEffect);
                inputEffect = Random.Range(0,effects.Count);
                orderLevel+=effects[inputEffect].levelReq;
                loop2+=1;
            }
        }*/
        /*string orderMessage = "Dear Merchant,\nI would like a ";
        //int randomNum = Random.Range(0,3);
        *//*switch(randomNum)
        {
            case 0:
            orderMessage+=""
            break;

            default:
            break;
        }*//*
        foreach(IngredientType effect in order)
        {
            orderMessage += effect.ingredientName + " ";
        }
        orderMessage+="poison";

        orderGiven.text = orderMessage;*/

        Debug.Log("Order is Given");
    }

    public void checkOrder()
    {
        List<IngredientType> potionEffectsIn = FindObjectOfType<Cauldron>().getFinalEffects();
        if (orderSequence[curOrderIndex].desiredEffects.All(i => potionEffectsIn.Contains(i)))
        {
            //level+=1;
            Debug.Log("Correct Poison");
            curOrderIndex++;
        }
        else
        {
            Debug.Log("Something was off");
        }


        //order = new List<IngredientType>{};
        FindObjectOfType<Cauldron>().resetEffects();
    }
}
