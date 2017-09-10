using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternFuel : MonoBehaviour {
	private Light light;

	public GameObject attachedParticleEmitter;
	public float fuel = 10; // seconds of fuel
	
	void Start () {
		light = GetComponent<Light>();
	}
	
	void Update () {
		// TODO: Flicker
		// TODO: Super flare
		// TODO: Decrease in area last few seconds, extra flicker
		fuel -= Time.deltaTime;
		
		if (fuel <= 0) {
			fuel = 0;
			light.enabled = false;
			attachedParticleEmitter.SetActive(false);
		}
	}

	public void addFuel(float amount) {
		fuel += amount;
		light.enabled = true;
		attachedParticleEmitter.SetActive(true);
	}
}
