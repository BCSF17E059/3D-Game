using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayGame : MonoBehaviour
{
    public GameObject[] gameScreens, environmentPart,levels,levelDescription;
    public GameObject environment, mainCam, crossHair, slider, descriptionBox;
    public int screenNo, environementNo, currenCoins;
    public Slider zoomValue;
    public float time;
    public Text TimeText, coins;
    private int minutes, seconds, myscene;
    public bool ifBird;
    public AudioClip buttonClip;
    int taskOn;
    // Start is called before the first frame update
    private void Awake()
    {
        descriptionBox.SetActive(false);
           taskOn = 0;
        slider.SetActive(false);
           currenCoins = 0;
        crossHair.SetActive(false);
     



        for (int i = 0; i < levels.Length; i++)
        {
            if (i == PlayerPrefs.GetInt("levelNo"))
            {
                levelDescription[i].SetActive(true);
                levels[i].SetActive(true);
            }
            else
            {
                levelDescription[i].SetActive(false);
                levels[i].SetActive(false);
            }
        }
        myscene = SceneManager.GetActiveScene().buildIndex;
        zoomValue.value = 0;
        Application.targetFrameRate = 60; 
        changeScreens(0);
        Invoke("selectEnvironment", 1f);
    }
   


    // Update is called once per frame
    void Update()
    {
coins.text = currenCoins.ToString();
        if (screenNo.Equals(1))
        {
            TimeFunction();
        }
    }
    void TimeFunction()
    {


        time -= Time.deltaTime;
        minutes = (int)time / 60;
        seconds = (int)time % 60;



        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (time <= 0)
        {
            time = 0;
            changeScreens(4);

        }

    }
    public  void changeScreens(int activeScreen)
    {
        screenNo = activeScreen;
        if (screenNo == 1)
        {

        }
        else
        {
            slider.SetActive(false);
            crossHair.SetActive(false);
        }
        for (int i = 0; i < gameScreens.Length; i++)
        {
            if (i == screenNo)
            {
                gameScreens[i].SetActive(true);
            }
            else
            {
                gameScreens[i].SetActive(false);
            }

        }
    }
    void selectEnvironment()
    {
        environment.SetActive(true);
        Invoke("startTheGame", 0.5f);
        mainCam.SetActive(false);
        if (PlayerPrefs.GetInt("levelNo") <= 2)
        {
            environementNo = 0;
        }
        if (PlayerPrefs.GetInt("levelNo") <= 5 && PlayerPrefs.GetInt("levelNo") > 2)
        {
            environementNo = 1;
        }
        if (PlayerPrefs.GetInt("levelNo") <= 8 && PlayerPrefs.GetInt("levelNo") > 5)
        {
            environementNo = 2;
        }
        if (PlayerPrefs.GetInt("levelNo") <= 11 && PlayerPrefs.GetInt("levelNo") > 8)
        {
            environementNo = 3;
        }
        for (int i = 0; i < environmentPart.Length; i++)
        {
            if (i == environementNo)
            {
                environmentPart[i].SetActive(true);
            }
            else
            {
                environmentPart[i].SetActive(false);
            }
        }
        time = FindObjectOfType<huntingScript>().levelTime;

    }
    void startTheGame()
        {
         changeScreens(1);
        }
    public void Level_Cleared()
    {
        PlayerPrefs.SetInt("GameScore", PlayerPrefs.GetInt("GameScore") + 50);
        if (PlayerPrefs.GetInt("levelNo") == 11)
        {
        }
        else
        {
            if (PlayerPrefs.GetInt("birdHunting") == 1)
            {
                PlayerPrefs.SetInt("birdlevel" + (PlayerPrefs.GetInt("levelNo") + 1), 1);
            }
            else
            {
                PlayerPrefs.SetInt("level" + (PlayerPrefs.GetInt("levelNo") + 1), 1);
            }
        }
            
        
        
       
      
        changeScreens(3);
    }
    public void next()
    {
        changeScreens(5);
       // PlayerPrefs.SetInt("LevelSelection", 1);
        Invoke("exit", 2f);

    }
    public void home()
    {
        changeScreens(5);
       // PlayerPrefs.SetInt("LevelSelection", 0);

        Invoke("exit", 2f);
    }
    void exit()
    {
        SceneManager.LoadScene("Menu");

    }
    public void RestartLEvel()
    {
        changeScreens(5);
        Invoke("restartScene", 2f);

    }
    void restartScene()
    {
        SceneManager.LoadScene(myscene, LoadSceneMode.Single);

    }
    public void buttonClick()
    {
        FindObjectOfType<soundScript>().gameSound.PlayOneShot(buttonClip, 1);
    }
    public void TaskOnOff()
    {
        if (taskOn == 0)
        {
            descriptionBox.SetActive(true);
            taskOn = 1;
        }
        else
        {
            descriptionBox.SetActive(false);
            taskOn = 0;
        }
       
    }
}
