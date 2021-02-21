using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuWork : MonoBehaviour
{
    public GameObject[] MenuScreens,Guns,GunsStats,GunTicks,priceText,levelBtn, birdlevelBtn;
    public GameObject purchase, next,playLevel,soundOn,soundOff,musicOn,musicOff, remove_btn, purchaseAll_btn;
    public int screenNo,gunNo, lvl_no,soundVal,musicVal;
    public Text coins;
    public bool birds;
    public AudioClip buttonClip;
    public AudioSource menuSoundBg;
    // Start is called before the first frame update
    void Start()
    { 
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            soundVal = 1;
        }
        else
        {
            soundVal = 0;
        }
        if (PlayerPrefs.GetInt("music") == 1)
        {
            musicVal = 1;
        }
        else
        {
            musicVal = 0;
        }
        if (PlayerPrefs.GetInt("UnclockAllGuns") == 1)
        {
            purchaseAll_btn.SetActive(false);
        }
        if (PlayerPrefs.GetInt("RemoveAD") == 1)
        {
            remove_btn.SetActive(false);
        }
       
        musicOnOff();
        sounOnOFf();
        playLevel.SetActive(false);
        changeGun(PlayerPrefs.GetInt("selectGun"));
           ChangeScreens(0);
        levelFuntion();

    }
    public void buttonClick()
    {
        menuSoundBg.PlayOneShot(buttonClip, 1);
    }
  
   public void ChangeScreens(int activeScreen)
    {
        coins.text = PlayerPrefs.GetInt("GameScore").ToString();
        screenNo = activeScreen;
        if (screenNo == 2 || screenNo == 3) 
        {

        }
        else
        {
            playLevel.SetActive(false);
        }
        for (int i = 0; i < MenuScreens.Length; i++)
        {
            if (i == screenNo)
            {
                MenuScreens[i].SetActive(true);
            }
            else
            {
                MenuScreens[i].SetActive(false);
            }

        }
    }
    public void changeGun(int activeGun)
    {
        gunNo = activeGun;
        for (int i = 0; i < Guns.Length; i++)
        {
            if (i == gunNo)
            {
                Guns[i].SetActive(true);
                GunsStats[i].SetActive(true);
                GunTicks[i].SetActive(true);
            }
            else
            {
                Guns[i].SetActive(false);
                GunsStats[i].SetActive(false);
                GunTicks[i].SetActive(false);
            }

        }
        birdlevelFuntion();
        levelFuntion();
        GunValidation(activeGun);

    }
     void GunValidation(int activeText)
    {
        coins.text = PlayerPrefs.GetInt("GameScore").ToString();
        //buyCharacter_Conditions....................................................................................................
        if (PlayerPrefs.GetInt("gun" + gunNo) == 1)
        {
            purchase.SetActive(false);
            next.SetActive(true);
            priceText[activeText].SetActive(false);
        }
        else
        {
            if (gunNo == 0)
            {
                priceText[activeText].SetActive(false);
                purchase.SetActive(false);
                next.SetActive(true);
            }
            else
            {
                priceText[activeText].SetActive(true);
                purchase.SetActive(true);
                next.SetActive(false);
            }
        }
    }
    public void Onclick_buy()
    {
        coins.text = PlayerPrefs.GetInt("GameScore").ToString();
        if (PlayerPrefs.GetInt("GameScore") >= (200 * gunNo))
        {
            PlayerPrefs.SetInt("GameScore", PlayerPrefs.GetInt("GameScore") - (200 * gunNo));
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("gun" + gunNo, 1);
            GunValidation(gunNo);
        }
        else
        {
          //  not_enough_cash.SetActive(true);
            Invoke("Not_E_cash", 2f);
        }
       
    }
    public void Not_E_cash()
    {
       // not_enough_cash.SetActive(false);
    }
    //SelectPlayers function----------------------------------------------------------------------------------------------------------------
    public void SelectPlayers()
    {
        PlayerPrefs.SetInt("selectGun", gunNo);

    
        ChangeScreens(0);
    }
    //onclickLevelBtn-----------------------------------------------------------------------------------------------------------------------
    public void Onclick_Level(int level_value)
    {
        playLevel.SetActive(true);
        PlayerPrefs.SetInt("levelNo", level_value);
      
        levelFuntion();
        birdlevelFuntion();
       // Invoke("PlayBtn", 0.5f);
    }
    void levelFuntion()
    {
        for (int i = 0; i < levelBtn.Length; i++)
        {
            if (PlayerPrefs.GetInt("level" + (i + 1)).Equals(1))
            {
                levelBtn[i + 1].GetComponent<Button>().interactable = true;
            }
        }
    }
    void birdlevelFuntion()
    {
        for (int i = 0; i < birdlevelBtn.Length; i++)
        {
            if (PlayerPrefs.GetInt("birdlevel" + (i + 1)).Equals(1))
            {
                birdlevelBtn[i + 1].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void ifBirdTrue(bool ifBird)
    {
        birds = ifBird;
    }
    //levelNextClick------------------------------------------------------------------------------------------------------------------
    public void PlayBtn()
    {
       
        print("2");
        ChangeScreens(6);
        Invoke("Loadlevel", 3f);
    }
    // loadlevel ------------------------------------------------------------------------------------------------------------------
    public void Loadlevel()
    {
        if (birds == true)
        {
            PlayerPrefs.SetInt("birdHunting", 1);
            SceneManager.LoadScene(2);
        }
        else
        {
            PlayerPrefs.SetInt("birdHunting", 0);
            SceneManager.LoadScene(1);
        }
      
    }
    public void sounOnOFf()
    {
        if (soundVal == 0)
        {
            PlayerPrefs.SetInt("sound", 0);
            menuSoundBg.enabled = false;
            soundOn.SetActive(true);
            soundOff.SetActive(false);
            soundVal = 1;
        }
        else if (soundVal == 1)
        {
            PlayerPrefs.SetInt("sound", 1);
            menuSoundBg.enabled = true;
            soundOn.SetActive(false);
            soundOff.SetActive(true);
            soundVal = 0;
        }
    }
    public void musicOnOff()
    {
        if (musicVal == 0)
        {
            PlayerPrefs.SetInt("music", 0);
            musicOn.SetActive(true);
            musicOff.SetActive(false);
            musicVal = 1;
        }
        else if (musicVal == 1)
        {
            PlayerPrefs.SetInt("music", 1);
            musicOn.SetActive(false);
            musicOff.SetActive(true);
            musicVal = 0;
        }
    }
    //exit Application------------------------------------------------------------------------------------------------------------------
    public void Exit_Yes_Button()
    {
        Application.Quit();
    }
    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=The+Art+World");
    }
    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/The-Art-Games-2163081070628590");
    }
    public void Twitter()
    {
        Application.OpenURL("https://twitter.com/TheArtGames1?lang=en");
    }
    public void Insta()
    {
        Application.OpenURL("https://www.instagram.com/theartgamingworld/?hl=en");
    }
    public void yes_remove_ad()
    {
        ChangeScreens(0);
        PlayerPrefs.SetInt("RemoveAD", 1);
      
            remove_btn.SetActive(false);
        
    }
    public void unlock_all_guns()
    {
        ChangeScreens(1);
        PlayerPrefs.SetInt("UnclockAllGuns", 1);
       
      
            purchaseAll_btn.SetActive(false);
        
        for (int i = 0; i < Guns.Length; i++)
        {
            PlayerPrefs.GetInt("gun" + (i +1), 1);
        }
    }

}
