using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour {

	Ray shootRay = new Ray();
	RaycastHit shootHit;
	int shootableMask;  
	public float range = 100f;
	public Transform cameraToShootFrom;
	public GameObject vfxEffect;

	// Use this for initialization
	void Start () {
		shootableMask = LayerMask.GetMask ("Shootable");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1")) {
			Shoot ();
		}
	}

	void Shoot() {
		shootRay.origin = cameraToShootFrom.position;
		shootRay.direction = cameraToShootFrom.forward;

		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			Transform deathPosition = shootHit.transform;
			GameObject effect = (GameObject)Instantiate (vfxEffect, deathPosition.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
			effect.GetComponent<ParticleSystem> ().Play ();
			Destroy(shootHit.transform.gameObject);
		}
	}
}
