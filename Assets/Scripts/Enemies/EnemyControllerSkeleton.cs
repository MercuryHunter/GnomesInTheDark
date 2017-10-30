using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerSkeleton : MonoBehaviour {

    enum State { Idle, Chase, Return, Dead, Attack }

    //variables
    public float MoveSpeed = 4.0f;
    int stateDistance;
    public GameObject guardPoint;
    State state;

    Animator animation;
    private UnityEngine.AI.NavMeshAgent agent;
    public GameObject[] Players;
    int InitCount = 1;
    float baseDistance;
    float[] PlayerDistances = { 0.0f, 0.0f, 0.0f, 0.0f };
    int currentTarget = 0;//Player1=0, Player2=1, Player3=2, Player4=3
    public float hurtTimer;

    bool CanAttack;
    float attackWait = 4.0f;
    public float waitTime = 8.0f;

    bool inSafeZone = false;

    void Start()
    {
        state = State.Idle;
        animation = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //Player = GameObject.FindGameObjectWithTag("Player");//Too slow to find object
        hurtTimer = 3f;
        stateDistance = 4;
        CanAttack = true;
    }


    void GetAgents()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (InitCount > 0) { GetAgents(); InitCount--; }

        if (!CanAttack)
        {
            attackWait -= Time.deltaTime;
            if (attackWait < 0)
            {
                CanAttack = true;
                attackWait = 4.0f;
            }
        }

        //Loop thorugh players lenght to see which is closest
        float MinDistToPlayer = 100.0f;
        for (int a = 0; a < Players.Length; a++)
        {

            PlayerDistances[a] = Vector3.Distance(Players[a].transform.position, transform.position);//Distance to player
            if (PlayerDistances[a] < MinDistToPlayer)
            {
                MinDistToPlayer = PlayerDistances[a];//Assign new closest distance
                currentTarget = a;//Set Current Player Target
            }
        }

        //Change to chase state if player gets within certain distance of enemy
        if (MinDistToPlayer < 10.0f)
        {
            state = State.Chase;
        }
        else if (MinDistToPlayer > 20) {
            state = State.Return;
        }

        switch (state)
        {
            case State.Idle:
                {
                    agent.SetDestination(gameObject.transform.position);
                    waitTime -= Time.deltaTime;
                    if (waitTime < 0)
                    {
                        state = State.Return;
                        waitTime = 8.0f;
                    }
                    break;
                }
            case State.Attack:
                {

                    break;
                }
            case State.Chase:
                {
                    agent.SetDestination(Players[currentTarget].transform.position);//Chases current target

                    if (MinDistToPlayer < 4 && CanAttack)
                    {
                        state = State.Attack;
                        //animation.CrossFade();
                        animation.SetTrigger("Attacking");
                        CanAttack = false;
                        Players[currentTarget].GetComponent<PlayerHealth>().Damage(10);
                    }
                    break;
                }
            case State.Return:
                {
                    agent.SetDestination(guardPoint.transform.position);
                    break;
                }
        }//End switch

        //Animate
        Animate(state);
    }



    void Animate(State s)
    {
        switch (s)
        {
            case State.Idle:
                {
                    animation.SetBool("IsWalking", false);
                    animation.SetBool("IsAttacking", false);
                    break;
                }
            case State.Chase:
                {
                    animation.SetBool("IsWalking", true);
                    break;
                }
            case State.Attack:
                {
                    animation.SetBool("IsAttacking", true);
                    animation.SetBool("IsWalking", false);
                    break;
                }
        }//End switch
    }


}
