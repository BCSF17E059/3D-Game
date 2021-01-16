using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
    
public class AnimalAIScript : MonoBehaviour
{

    public NavMeshAgent navmeshAgent;
    public Animation anim;
    public  int number, stopInc,stopend;
    float time = 0.1f;
    public Transform player;
    public GameObject cubeOfBullet;
    public Transform[] destiPoints;
    public bool noFight,getHit, fight,ifFight;
    int stophit,stopendfight;
    // Start is called before the first frame update
    private void Awake()
    {
        stopendfight = 0;
           stophit = 0;
        cubeOfBullet.SetActive(false);
           player = GameObject.FindGameObjectWithTag("Player").transform;
           anim = this.gameObject.GetComponent<Animation>();
        navmeshAgent = this.GetComponent<NavMeshAgent>();
       
    }
    void Start()
    {
       
        //  FindObjectOfType<huntScript>().DestinationPoint.SetActive(false);
        stopend = 0;
        number = Random.Range(0, destiPoints.Length - 1);
        stopInc = 0;
        if (navmeshAgent == null )
        {
            Debug.LogError("NavMeshAgent is missing : " + this.gameObject.name);
        }
        else
        {
            DestinationPoints(number);
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (getHit == true)
        {
            if (stophit == 0)
            {
                navmeshAgent.enabled = false;
                anim.CrossFade("getHit");
                Invoke("hitfalse", 0.5f);
                if (ifFight == true)
                {
                    fight = true;
                }
                else
                {
                    noFight = false;
                }
                stophit = 1;
            }
        }
        else
        {
            if (fight == true)
            {
                if (Vector3.Distance(this.gameObject.transform.position, player.position) < 7f)
                {
                    if (stopendfight == 0)
                    {
                        FindObjectOfType<PlayGame>().changeScreens(6);
                        navmeshAgent.enabled = false;
                        anim.CrossFade("Attack");
                        Invoke("levelFailed", 1f);
                        stopendfight = 1;
                    }
                  
                }
                else
                {
                    navmeshAgent.enabled = true;
                    anim.CrossFade("run");
                    Vector3 targets = player.position;
                    navmeshAgent.speed = 4f;
                    navmeshAgent.SetDestination(targets);
                }
                    
            }
            else
            {
                if (noFight == true)
                {
                    allPlayer();
                }
                else
                {
                    if (ifFight == true)
                    {
                        noFight = true;
                    }
                    else
                    {
                        navmeshAgent.enabled = true;
                        anim.CrossFade("run");
                        Vector3 targets = destiPoints[destiPoints.Length - 1].position;
                        navmeshAgent.speed = 4f;
                        navmeshAgent.SetDestination(targets);
                    }
                        
                }
            }
          
           
        }
        
    
        
       
      
      

    }
    
