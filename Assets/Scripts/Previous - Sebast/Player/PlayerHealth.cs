using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

//Script taken and modified from Unity Survival Shooter Tutorial Project


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;

    public Slider healthSlider2;
	//public Slider healthSlider3;
	//public Slider healthSlider1;

   	//public Image damageImage;
    public AudioClip deathClip;

    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


   	
    AudioSource playerAudio;
    PlayerIsometricController player2Movement;
	PlayerThirdPersonController player3Movement;
	PlayerFirstPersonController player1Movement;

	PlayerCamController pcc;

    //PlayerShooting playerShooting;
    bool Alive;
    bool damaged;


    void Awake ()
    {
        playerAudio = GetComponent <AudioSource> ();

        player2Movement = GetComponent <PlayerIsometricController> ();
		player3Movement = GetComponent <PlayerThirdPersonController> ();
		player1Movement = GetComponent <PlayerFirstPersonController> ();
		pcc = GetComponent<PlayerCamController> ();

        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
		Alive = true;
    }


    void Update ()
    {
        if(damaged)
        {
            //damageImage.color = flashColour;
        }
        else
        {
            //damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider2.value = currentHealth;
		//healthSlider3.value = currentHealth;
		//healthSlider1.value = currentHealth;

        playerAudio.Play ();

        //if(currentHealth <= 0 && Alive)
		if(currentHealth <= 0 )
        {
            Die ();
        }
    }


    void Die ()
    {
        Alive = false;
		pcc.Died ();
        //playerShooting.DisableEffects ();

		DieAnimation ();

        playerAudio.clip = deathClip;
        playerAudio.Play ();

		player2Movement.enabled = false;
		player3Movement.enabled = false;
		player1Movement.enabled = false;

        //playerShooting.enabled = false;
    }


	void DieAnimation(){
		QuerySDEmotionalController.QueryChanSDEmotionalType face = QuerySDEmotionalController.QueryChanSDEmotionalType.NORMAL_SAD;
		ChangeFace (face);
		ChangeAnimation (57);
		
	}

	void ChangeFace (QuerySDEmotionalController.QueryChanSDEmotionalType faceNumber) {
		gameObject.GetComponent<QuerySDEmotionalController>().ChangeEmotion(faceNumber);
	}
		
	void ChangeAnimation (int animNumber){
		gameObject.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)animNumber);
	}


}
