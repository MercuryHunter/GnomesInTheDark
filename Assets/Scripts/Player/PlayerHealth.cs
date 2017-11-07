using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	
	public int startingHealth = 100;
	private int currentHealth;
	public int captureDamage = 5;
	public Slider healthSlider;

	private bool captured;
	public float secondsToDeathFromFullHealth = 300;
	public float timeBetweenCaptureDamage;
	private float timeToNextCaptureDamage;
	private int currentCaptureCounter;
	private bool dead;

	private EndStateController endStateController;
	private PlayerMovement playerMovement;
	
	void Start () {
		currentHealth = startingHealth;
		healthSlider.maxValue = startingHealth;
		healthSlider.value = currentHealth;
		timeToNextCaptureDamage = 5;
		currentCaptureCounter = 0;
		timeBetweenCaptureDamage = secondsToDeathFromFullHealth / (startingHealth / captureDamage);

		endStateController = GetComponent<EndStateController>();
		playerMovement = GetComponent<PlayerMovement>();
	}
	
	void Update () {
		if (dead) return;
		
		// TODO: Start taking damage from capture
		if (captured) {
			timeToNextCaptureDamage -= Time.deltaTime;
			if (timeToNextCaptureDamage <= 0) {
				Damage(captureDamage);
				currentCaptureCounter++;
				timeToNextCaptureDamage = timeBetweenCaptureDamage;
			}
		}
	}

	public void Heal(int amount) {
		currentHealth += amount;
		healthSlider.value = currentHealth;
	}

	public void Damage(int amount) {
		Debug.Log("Player took " + amount + " damage.");
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		
		if (currentHealth <= 0 && !dead) Die();
	}

	private void Die() {
		currentHealth = 0;
		dead = true;
		endStateController.Die();
		playerMovement.disallowMovement();
		playerMovement.disallowJump();
		// TODO: Animation
	}

	public void Capture() {
		captured = true;
		Damage(captureDamage);
	}

	public void Release() {
		captured = false;
		currentCaptureCounter = 0;
		timeToNextCaptureDamage = timeBetweenCaptureDamage;
	}
	
	public bool isDead() {
		return dead;
	}
}
