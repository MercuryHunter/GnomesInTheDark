﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyControllerRED : MonoBehaviour, EnemyScript {

    enum State { Idle, Chase, Return, Dead, RUN }
    enum ChaseState { SCARED, BRAVER, IMMUNE }
    //variables
    public float MoveSpeed = 4.0f;
    int stateDistance;
    public GameObject slimeBase;
    public GameObject guardPoint;
    State state;
    ChaseState chaseState;
    Animation animation;
    private NavMeshAgent agent;
    public GameObject [] Players;
    int InitCount = 1;
    float baseDistance;
    float [] PlayerDistances = { 0.0f, 0.0f, 0.0f, 0.0f };
    bool[] playerChase = { true, true, true, true };
    int currentTarget = 0;//Player1=0, Player2=1, Player3=2, Player4=3
    int capturedPlayerNum = 0;
    private bool hasPlayer;
    private GameObject capturedPlayer;
    public float hurtTimer;
    private float runTimer;
    private float runAwayTime;
    bool inSafeZone = false;

    void Start () {
        state = State.Idle;
        animation = GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        //Player = GameObject.FindGameObjectWithTag("Player");//Too slow to find object
        hasPlayer = false;
        capturedPlayer = null;
        hurtTimer = 3f;
        baseDistance = Vector3.Distance(slimeBase.transform.position, transform.position);
        stateDistance = 4;
        runTimer = 0;
        runAwayTime = 10;
        chaseState = ChaseState.SCARED;
    }


    public void GetAgents() {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }

	// Update is called once per frame
	void Update () {

        if (InitCount > 0) { GetAgents();  InitCount--; }


        //Loop thorugh players lenght to see which is closest
        if (state != State.RUN)
        {
            float MinDistToPlayer = 30.0f;
            float distToLantern;
            for (int a = 0; a < Players.Length; a++)
            {
                if (playerChase[a])
                {
                    PlayerDistances[a] = Vector3.Distance(Players[a].transform.position, transform.position);//Distance to player
                    if ((PlayerDistances[a] < MinDistToPlayer) && (Players[a].GetComponentInChildren<Light>() != null))
                    {
                        MinDistToPlayer = PlayerDistances[a];//Assign new closest distance
                        print(a);
                        currentTarget = a;//Set Current Player Target
                        float lanternToPlayer = Players[a].GetComponentInChildren<Light>().range;
                       // print("Lantern "+lanternToPlayer);
                       // print("player" +MinDistToPlayer);
                        if ((MinDistToPlayer < (lanternToPlayer - stateDistance) && Players[a].GetComponentInChildren<LanternFuel>().isOn()) || inSafeZone)
                        {
                           // print("should run");
                            state = State.RUN;
                            inSafeZone = false;
                            if (hasPlayer) {
                                freePlayer();
                                print("Player is released");
                            }
                            
                        }
                    }
                }
            }

            baseDistance = Vector3.Distance(slimeBase.transform.position, transform.position);

            if (state != State.Return && state != State.RUN)
            {
                //Change to chase state if player gets within certain distance of enemy
                if (MinDistToPlayer < 10.0f)
                {
                    state = State.Chase;
                }
                else { state = State.Idle; }
            }


            switch (state)
            {
                case State.Idle:
                    {
                        agent.SetDestination(guardPoint.transform.position);
                        break;
                    }
                case State.Chase:
                    {

                        agent.SetDestination(Players[currentTarget].transform.position);//Chases current target
                        break;
                    }
                case State.Return:
                    {
                        agent.SetDestination(slimeBase.transform.position);

                        if (baseDistance <= 6)
                        {
                            releasePlayer();
                        }
                        break;
                    }
                case State.RUN:
                    {
                        print(chaseState);
                        
                        switch (chaseState)
                        {
                            case ChaseState.SCARED:
                                {
                                    print("got into scared");
                                    Vector3 tempDirection = transform.position - Players[currentTarget].transform.position;
                                    Vector3 runToLocation = new Vector3(transform.position.x + tempDirection.x, transform.position.y + tempDirection.y, transform.position.z + tempDirection.z);
                                    agent.SetDestination(runToLocation);
                                    chaseState = ChaseState.BRAVER;
                                    runAwayTime = 10;
                                    break;
                                }
                            case ChaseState.BRAVER:
                                {
                                    print("got into Brave");
                                    //agent.SetDestination(new Vector3((Players[currentTarget].transform.position.x - 3), Players[currentTarget].transform.position.y, Players[currentTarget].transform.position.z - 3));
                                    Vector3 tempDirection = transform.position - Players[currentTarget].transform.position;
                                    Vector3 runToLocation = new Vector3(transform.position.x + tempDirection.x, transform.position.y + tempDirection.y, transform.position.z + tempDirection.z);
                                    agent.SetDestination(runToLocation);
                                    stateDistance = 6;
                                    chaseState = ChaseState.IMMUNE;
                                    runAwayTime = 5;
                                    break;
                                }
                            case ChaseState.IMMUNE:
                                {
                                    print("got into immune");
                                    agent.SetDestination(Players[currentTarget].transform.position);//Chases current target
                                    break;
                                }

                        }
                        animation.CrossFade("Walk");
                        break;
                    }
            }//End switch
        }
        if (hasPlayer && capturedPlayer != null)
        {
            hurtTimer -= Time.deltaTime;
            if (hurtTimer < 0)
            {
                capturedPlayer.GetComponent<PlayerHealth>().Damage(10);
                hurtTimer = 3f;
            }
           // transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z);
        }

        else if (state == State.RUN)
        {
            if (ChaseState.IMMUNE != chaseState)
            {
                Vector3 tempDirection = transform.position - Players[currentTarget].transform.position;
                Vector3 runToLocation = new Vector3(transform.position.x + tempDirection.x, transform.position.y + tempDirection.y, transform.position.z + tempDirection.z);
                agent.SetDestination(runToLocation);
            }
           
            runTimer += Time.deltaTime;
            if (runTimer > runAwayTime)
            {
                runTimer = 0;
                state = State.Idle;
            }
        }

        //Animate
        Animate(state);
	}



    void Animate(State s) {
        switch (s)
        {
            case State.Idle:
                {
                    animation.CrossFade("Wait");
                    break;
                }
            case State.Chase:
                {
                    animation.CrossFade("Walk");
                    break;
                }
            case State.Return:
                {
                    animation.CrossFade("Walk");
                    break;
                }
        }//End switch
    }

    public void releasePlayer()
    {
        capturedPlayer.transform.position = slimeBase.transform.position;
        state = State.Idle;
        capturedPlayer.GetComponent<PlayerMovement>().disallowMovement();
        capturedPlayer.transform.parent = null;
        PlayerDistances[capturedPlayerNum] = 100f;
        hasPlayer = false;
        slimeBase.GetComponent<SlimeBaseController>().addPlayer(capturedPlayer, capturedPlayerNum, gameObject);
        // agent.SetDestination(transform.position);
        capturedPlayer = null; 
    }

    public void freePlayer()
    {
        capturedPlayer.GetComponent<PlayerMovement>().allowMovement();
        capturedPlayer.transform.FindChild("Lantern").gameObject.SetActive(true);
        capturedPlayer.transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z - 3);
        capturedPlayer.transform.parent = null;
        Image[] images = capturedPlayer.GetComponentsInChildren<Image>();
        // print(images.Length);
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].gameObject.name == "SlimeCover")
            {
                images[i].gameObject.GetComponent<Image>().enabled = true;

            }
        }
        capturedPlayer = null;
        playerChase[capturedPlayerNum] = true;
        hasPlayer = false;
        chaseState = ChaseState.BRAVER;

    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            if (!hasPlayer)
            {
                playerChase[currentTarget] = false;
                capturedPlayerNum = currentTarget;
                state = State.Return;
                capturedPlayer = other.gameObject;
                other.transform.parent = this.transform;
                hasPlayer = true;
                capturedPlayer.GetComponent<PlayerMovement>().disallowMovement();
                capturedPlayer.transform.FindChild("Lantern").gameObject.SetActive(false);
                capturedPlayer.transform.position = transform.position;
                //capturedPlayers.GetComponentInChildren<BoxCollider>().enabled = false;
                //other.transform.FindChild("slimeCover").gameObject.GetComponent<Image>().enabled = true;
                Image[] images = capturedPlayer.GetComponentsInChildren<Image>();
                // print(images.Length);
                for (int i = 0; i < images.Length; i++)
                {
                    if (images[i].gameObject.name == "SlimeCover")
                    {
                        images[i].gameObject.GetComponent<Image>().enabled = true;

                    }
                }
            }
        }
        if (other.gameObject.tag == "SafeZone")
        {
            //print("Went into run");
            inSafeZone = true;
            //state = State.RUN;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SafeZone")
        {
            //print("Went into run");
            inSafeZone = false;
            //state = State.RUN;
        }
    }
    public void releasePlayerNum(int playerNumberRelease)
    {
        playerChase[playerNumberRelease] = true;
    }
}
