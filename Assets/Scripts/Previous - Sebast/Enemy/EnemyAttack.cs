using UnityEngine;
using System.Collections;


//Script taken and modified from Unity Survival Shooter Tutorial Project

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    GameObject player;
    PlayerHealthOLD _playerHealthOld;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        _playerHealthOld = player.GetComponent <PlayerHealthOLD> ();
        enemyHealth = GetComponent<EnemyHealth>();
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            HitPlayer ();
        }

		if(_playerHealthOld.currentHealth <= 0)
        {
			//AnimationPlayerDead ();
//			GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
//			GetComponent <Rigidbody> ().isKinematic = true;
        }
    }

    void HitPlayer ()
    {
        timer = 0f;

        if(_playerHealthOld.currentHealth > 0)
        {
            _playerHealthOld.TakeDamage (attackDamage);
        }
    }

/*
	void AnimationAttack(){
		QuerySDEmotionalController.QueryChanSDEmotionalType face = QuerySDEmotionalController.QueryChanSDEmotionalType.NORMAL_GURUGURU;
		ChangeFace (face);
		ChangeAnimation (106);
	}

	void AnimationPlayerDead(){
		QuerySDEmotionalController.QueryChanSDEmotionalType face = QuerySDEmotionalController.QueryChanSDEmotionalType.NORMAL_GURUGURU;
		ChangeFace (face);
		ChangeAnimation (106);
	}

	void ChangeFace (QuerySDEmotionalController.QueryChanSDEmotionalType faceNumber) {
		gameObject.GetComponent<QuerySDEmotionalController>().ChangeEmotion(faceNumber);
	}

	void ChangeAnimation (int animNumber){
		gameObject.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)animNumber);
	}
*/

}
