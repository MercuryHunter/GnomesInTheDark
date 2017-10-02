using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class LanternFuel : MonoBehaviour {
	private Light lanternLight;
	private GameObject attachedParticleEmitter;

	//public GameObject attachedParticleEmitter;
	public float maxFuel = 100;
	public float currentFuel = 10; // seconds of fuel
	public float fuelUsageRatePerSecond = 1f;
	public float fuelUsageModifier = 0.2f;

	public Slider fuelSlider;

	public float intensity = 3.0f;
	public float minRange = 8.0f;
	public float maxRange = 12.0f;
	public float rangeModifier = 1.0f;
	public float minMinRange = 5.0f;
	public float maxMaxRange = 15.0f;
	public float maxFlickerDifference = 0.1f;

	private bool on;
	
	private BaseController controller;
	private SphereCollider lightCollider;
	
	void Start () {
		// Get child flameobject of lantern
		GameObject myFlameObject = null;
		GameObject[] flameObjects = GameObject.FindGameObjectsWithTag("LanternFlame");
		foreach(GameObject flameObject in flameObjects)
			if (flameObject.transform.IsChildOf(this.transform))
				myFlameObject = flameObject;

		if (myFlameObject == null) {
			Debug.Log("Where is my flame?\nAHHHHHHHHHH!");
		}
		
		lanternLight = myFlameObject.GetComponent<Light>();
		attachedParticleEmitter = myFlameObject.transform.GetChild(0).gameObject;
		// It is assumed the particle emitter is the first child of the flame.
		
		lanternLight.intensity = intensity;
		on = true;

		fuelSlider.maxValue = maxFuel;
		fuelSlider.value = currentFuel;
		
		controller = GetComponentInParent<BaseController>();
		lightCollider = GetComponent<SphereCollider>();
	}
	
	void Update () {
		// TODO: Super flare
		// TODO: Decrease in area last few seconds, extra flicker
		if (on) {
			currentFuel -= Time.deltaTime * fuelUsageRatePerSecond;
			fuelSlider.value = currentFuel;
			
			// Some flickering
			// Tinker with variables at top to affect this.
			float newRange = Random.Range(
				lanternLight.range - maxFlickerDifference,
				lanternLight.range + maxFlickerDifference
			);
			lanternLight.range = Mathf.Clamp(newRange, minRange, maxRange);
			lightCollider.radius = lanternLight.range;
			
			if (currentFuel <= 0) {
				// Disable if out of fuel
				currentFuel = 0;
				turnOff();
			}
			
			if (controller.increaseLight()) changeRange(rangeModifier);
			if (controller.decreaseLight()) changeRange(-rangeModifier);
			if (controller.toggleLight()) turnOff();
			/*
			if (Input.GetKeyDown(KeyCode.Equals)) changeRange(rangeModifier);
			if (Input.GetKeyDown(KeyCode.Minus)) changeRange(-rangeModifier);
			if (Input.GetKeyDown(KeyCode.F)) turnOff();
			*/
		}
		else {
			if (controller.toggleLight()) turnOn();
		}
	}

	private void changeRange(float amount) {
		if (minRange + amount < minMinRange) return;
		if (maxRange + amount > maxMaxRange) return;
		
		minRange += amount;
		maxRange += amount;
		
		lanternLight.range = (minRange + maxRange) / 2;

		fuelUsageRatePerSecond += Mathf.Sign(amount) * fuelUsageModifier;
	}

	private void turnOff() {
		on = false;
		lanternLight.enabled = false;
		attachedParticleEmitter.SetActive(false);
	}

	private void turnOn() {
		if (currentFuel > 0) {
			on = true;
			lanternLight.enabled = true;
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
