using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script taken and modified from Unity Survival Shooter Tutorial Project


public class EnemyController : MonoBehaviour {

	Transform target;
	GameObject player;

	UnityEngine.AI.NavMeshAgent nav;
	bool Alive;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;

		player = GameObject.FindGameObjectWithTag ("Player");

		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		Alive = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Alive && (player.GetComponent<PlayerHealthOLD>().currentHealth) > 0) {
			nav.SetDestination (target.position);
		}


		Animation ();
	}

	void Animation(){


		float h = gameObject.transform.position.x;
		float v = gameObject.transform.position.y;


		bool isMoving = h != 0.0f || v != 0.0f;
		/*
		if (isMoving) {//Is moving
			ChangeAnimation (2);
		} else {
			ChangeAnimation (1);//Not running idle
		}
		*/

		//When dies
		if (!Alive) {
			Vector3 dead = new Vector3 (h, 0.0f, v);
			gameObject.transform.TransformPoint (dead);
			/*
			ChangeAnimation (57);
			QuerySDEmotionalController.QueryChanSDEmotionalType face = QuerySDEmotionalController.QueryChanSDEmotionalType.NORMAL_SAD;
			ChangeFace (face);
			*/
		}

	}

	public void Died(){
		Alive = false;
	}

/*
	void ChangeFace (QuerySDEmotionalController.QueryChanSDEmotionalType faceNumber) {

		gameObject.GetComponent<QuerySDEmotionalController>().ChangeEmotion(faceNumber);

	}


	void ChangeAnimation (int animNumber)
	{
		gameObject.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)animNumber);
	}
*/
}
