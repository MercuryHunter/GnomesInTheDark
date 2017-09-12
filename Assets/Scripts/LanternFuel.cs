using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class LanternFuel : MonoBehaviour {
	private Light light;

	public GameObject attachedParticleEmitter;
	public float maxFuel = 100;
	public float currentFuel = 10; // seconds of fuel
	public float fuelUsageRatePerSecond = 1f;
	public float fuelUsageModifier = 0.2f;
	// TODO: Fix usage rate infinity

	public Slider fuelSlider;

	public float intensity = 3.0f;
	public float minRange = 8.0f;
	public float maxRange = 12.0f;
	public float rangeModifier = 1.0f;
	// TODO: Max max range
	public float maxFlickerDifference = 0.1f;

	private bool on;
	
	void Start () {
		light = GetComponent<Light>();
		light.intensity = intensity;
		on = true;

		fuelSlider.maxValue = maxFuel;
		fuelSlider.value = currentFuel;
	}
	
	void Update () {
		// TODO: Super flare
		// TODO: Decrease in area last few seconds, extra flicker
		if (on) {
			currentFuel -= Time.deltaTime * fuelUsageRatePerSecond;
			fuelSlider.value = currentFuel;
			
			// Some flickering
			// Tinker with variables at top to affect this.
			float newRange = Random.Range(light.range - maxFlickerDifference, light.range + maxFlickerDifference);
			light.range = Mathf.Clamp(newRange, minRange, maxRange);
			
			if (currentFuel <= 0) {
				// Disable if out of fuel
				currentFuel = 0;
				turnOff();
			}

			if (Input.GetKeyDown(KeyCode.Equals)) changeRange(rangeModifier);
			if (Input.GetKeyDown(KeyCode.Minus)) changeRange(-rangeModifier);
			if (Input.GetKeyDown(KeyCode.F)) turnOff();
		}
		else {
			if (Input.GetKeyDown(KeyCode.F)) turnOn();
		}
	}

	private void changeRange(float amount) {
		minRange += amount;
		maxRange += amount;
		
		light.range = (minRange + maxRange) / 2;

		fuelUsageRatePerSecond += Mathf.Sign(amount) * fuelUsageModifier;
	}

	private void turnOff() {
		on = false;
		light.enabled = false;
		attachedParticleEmitter.SetActive(false);
	}

	private void turnOn() {
		if (currentFuel > 0) {
			on = true;
			light.enabled = true;
			attachedParticleEmitter.SetActive(true);
		}
	}

	public void addFuel(float amount) {
		// Enable since we have fuel if we didn't before
		// This is a design choice, players who had 0 fuel had their light on before,
		// so it makes sense to re-enable after getting more fuel 
		if (currentFuel <= 0) {
			currentFuel += amount;
			turnOn();
		}
		else {
			currentFuel += amount;
		}
	}
}
