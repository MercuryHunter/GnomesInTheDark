using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using Random = UnityEngine.Random;

public class LanternFuel : MonoBehaviour {
	private Light light;

	public GameObject attachedParticleEmitter;
	public float fuel = 10; // seconds of fuel

	public float minRange = 8;
	public float maxRange = 12;
	public float maxFlickerDifference = 0.1f;

	private bool on;
	
	void Start () {
		light = GetComponent<Light>();
		on = true;
	}
	
	void Update () {
		// TODO: Super flare
		// TODO: Decrease in area last few seconds, extra flicker
		if (on) {
			fuel -= Time.deltaTime;
			
			// Some flickering
			// Tinker with variables at top to affect this.
			float newRange = Random.Range(light.range - maxFlickerDifference, light.range + maxFlickerDifference);
			light.range = Mathf.Clamp(newRange, minRange, maxRange);
			
			if (fuel <= 0) {
				// Disable if out of fuel
				fuel = 0;
				turnOff();
			}

			if (Input.GetKeyDown(KeyCode.F)) turnOff();
		}
		else {
			if (Input.GetKeyDown(KeyCode.F)) turnOn();
		}
	}

	private void turnOff() {
		on = false;
		light.enabled = false;
		attachedParticleEmitter.SetActive(false);
	}

	private void turnOn() {
		if (fuel > 0) {
			on = true;
			light.enabled = true;
			attachedParticleEmitter.SetActive(true);
		}
	}

	public void addFuel(float amount) {
		// Enable since we have fuel if we didn't before
		// This is a design choice, players who had 0 fuel had their light on before,
		// so it makes sense to re-enable after getting more fuel 
		if (fuel <= 0) {
			fuel += amount;
			turnOn();
		}
		else {
			fuel += amount;
		}
	}
}
