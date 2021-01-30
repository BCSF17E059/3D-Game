using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onBulletEnters : MonoBehaviour
{
  public  int stop,onceAgain;
    public static bool tryAgain, boolTriggerd;
    // Start is called before the first frame update
    void Start()
    {
        boolTriggerd = false;
           tryAgain = false;
           onceAgain = 0;
        stop = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (onceAgain == 0)
        {
            if (FindObjectOfType<DamageManager>().dead == false&&onBulletEnters.boolTriggerd == true&&tryAgain==true)
            {
                boolTriggerd = false;
           
                stop = 0;
                FindObjectOfType<AS_ActionCamera>().CameraRestore();
                FPSController.crossHairOff = false;
             
                FindObjectOfType<DamageManager>().dead = false;
               // Invoke("restoreCamera", 1f);
                tryAgain = false;
                AS_ActionCamera.stopDetection = 0;
                onceAgain = 1;
                FindObjectOfType<AS_ActionCamera>().gameObject.transform.position = FindObjectOfType<huntingScript>().Player.transform.position;
            }
           
        }
        
    }
    void restoreCamera()
    {
        FindObjectOfType<huntingScript>().actionCambullet.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (stop == 0)
        {
            if (other.gameObject.tag == "Bullet")
            {
                print("bulletTrigger");
                boolTriggerd = true;
                   onceAgain = 0;
                //  other.gameObject.GetComponent<MeshCollider>().enabled = false;
                //   FindObjectOfType<AS_ActionCamera>().stopit = 1;

                //FindObjectOfType<AS_ActionCamera>().Follow=false;
                FindObjectOfType<AS_ActionCamera>().ClearTarget();
            
               stop = 1;

            }
        }
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
}
