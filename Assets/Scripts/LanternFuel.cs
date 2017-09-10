using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class LanternFuel : MonoBehaviour {
	private Light light;

	public GameObject attachedParticleEmitter;
	public float fuel = 10; // seconds of fuel

	public float minRange = 8;
	public float maxRange = 12;
	public float maxFlickerDifference = 0.1f;
	
	void Start () {
		light = GetComponent<Light>();
	}
	
	void Update () {
		// TODO: Super flare
		// TODO: Decrease in area last few seconds, extra flicker
		fuel -= Time.deltaTime;

		if (fuel <= 0) {
			// Disable if out of fuel
			fuel = 0;
			light.enabled = false;
			attachedParticleEmitter.SetActive(false);
		}
		else {
			// Some flickering
			// Tinker with variables at top to affect this.
			float newRange = Random.Range(light.range - maxFlickerDifference, light.range + maxFlickerDifference);
			light.range = Mathf.Clamp(newRange, minRange, maxRange);
		}
	}

	public void addFuel(float amount) {
		// Enable since we have fuel
		fuel += amount;
		light.enabled = true;
		attachedParticleEmitter.SetActive(true);
	}
}
