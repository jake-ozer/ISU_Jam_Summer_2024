using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public float time;
    public float FinalTime = 60f;
    public int NumStages = 100;
    private bool startFade = false;
    private bool reverseFade = false;
    private int stage;
    //public Material fadeScreenMat;
    public GameObject fadeScreen;
    // Start is called before the first frame update
    void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            reverseFade = true;
            time=0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(startFade)
        {
            if(time>=FinalTime)
            {
                SceneManager.LoadScene(1);
            }
            else if(time>=FinalTime/(float)NumStages*(float)stage)
            {
                stage+=1;
                Color newColor = new Color(0f, 0f, 0f, (float)stage/(float)NumStages);
                fadeScreen.GetComponent<Image>().color = newColor;
            }
            else
            {
                time+=Time.deltaTime;
            }
        }

        if(reverseFade)
        {
            if(time>=FinalTime)
            {
                fadeScreen.SetActive(false);
            }
            else if(time>=FinalTime/(float)NumStages*(float)stage)
            {
                stage+=1;
                Color newColor = new Color(0f, 0f, 0f, ((float)NumStages - (float)stage)/(float)NumStages);
                fadeScreen.GetComponent<Image>().color = newColor;
            }
            else
            {
                time+=Time.deltaTime;
            }
        }
    }

    public void startGame()
    {
        fadeScreen.SetActive(true);
        time=0.0f;
        startFade=true;
        stage = 0;
        //SceneManager.LoadScene(1);
    }
}
