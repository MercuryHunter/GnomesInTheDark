using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndStateController : MonoBehaviour {
	
	public Sprite deathImage;
	public Sprite escapedImage;

	public Image endStateOverlay;

	public float duration = 5.0f;

	private float startTime;
	private Sprite changingTo;
	
	public void Update() {
		if (changingTo == null) return;
		
		float t = (Time.time - startTime) / duration;
		endStateOverlay.color = new Color(1f,1f,1f,Mathf.SmoothStep(0f, 1f, t));
	}
	
	public void Die() {
		changingTo = deathImage;
		startTime = Time.time;
		endStateOverlay.sprite = deathImage;
	}

	public void Escape() {
		changingTo = escapedImage;
		startTime = Time.time;
		endStateOverlay.sprite = escapedImage;
	}
}
