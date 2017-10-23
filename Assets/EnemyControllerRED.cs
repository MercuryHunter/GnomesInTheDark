using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerRED : MonoBehaviour {

    enum State { Idle, Chase, Return, Dead }

    //variables
    public float MoveSpeed = 4.0f;
    State state;
    Animation animation;
    private NavMeshAgent agent;
    public GameObject [] Players;
    int InitCount = 1;
    float [] PlayerDistances = { 0.0f, 0.0f, 0.0f, 0.0f };
    int currentTarget = 0;//Player1=0, Player2=1, Player3=2, Player4=3
	

	void Start () {
        state = State.Idle;
        animation = GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        //Player = GameObject.FindGameObjectWithTag("Player");//Too slow to find object
        
    }


    void GetAgents() {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }

	// Update is called once per frame
	void Update () {

        if (InitCount > 0) { GetAgents();  InitCount--; }


        //Loop thorugh players lenght to see which is closest
        float MinDistToPlayer = 100.0f;
        for (int a = 0; a < Players.Length; a++) {
            PlayerDistances[a] = Vector3.Distance(Players[a].transform.position, transform.position);//Distance to player
            if (PlayerDistances[a] < MinDistToPlayer) {
                MinDistToPlayer = PlayerDistances[a];//Assign new closest distance
                currentTarget = a;//Set Current Player Target
            }
        }


        //Change to chase state if player gets within certain distance of enemy
        if (MinDistToPlayer < 10.0f) {
            state = State.Chase;
        }
        else { state = State.Idle; }
        

        switch (state) {
            case State.Idle:
                {

                    break;
                }
            case State.Chase:
                {
                    agent.SetDestination(Players[currentTarget].transform.position);//Chases current target
                    break;
                }
            case State.Return:
                {

                    break;
                }
        }//End switch

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


}
