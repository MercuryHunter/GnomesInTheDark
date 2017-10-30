using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour {

    private float soundTimer;
    private int timeSwitch;
    public AudioSource[] allSources;

    private void Start()
    {
        soundTimer = 0;
        timeSwitch = Random.Range(60, 180);
    }

    private void Update()
    {
        soundTimer += Time.deltaTime;
        if (soundTimer > timeSwitch)
        {
            soundTimer = 0;
            timeSwitch = Random.Range(60, 180);
            int selectedSource = Random.Range(0, 3);
            allSources[selectedSource].Play();
        }
    }
}
