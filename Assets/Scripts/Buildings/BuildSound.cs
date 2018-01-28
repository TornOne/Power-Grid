using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSound : MonoBehaviour {

    public AudioClip buildSound;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play() {
        AudioManager.GetAudioManager().PlayBuild(buildSound);
    }
}
