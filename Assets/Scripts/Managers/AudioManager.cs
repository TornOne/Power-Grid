using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public AudioClip BuildWire;
    public AudioClip BuildWind;
    public AudioClip BuildNuclear;
    public AudioClip UISell;
    public AudioClip UISelect;
    public AudioClip UIDenied;
    public AudioClip UIUpgrade;

    public AudioSource Wiresource;
    public AudioSource Windsource;
    public AudioSource Nuclearsource;
    public AudioSource Sellsource;
    public AudioSource Selectsource;
    public AudioSource Deniedsource;
    public AudioSource Upgradesource;

    private static AudioManager audioManager;

    void Start () {
        Wiresource.clip = BuildWire;
        Windsource.clip = BuildWind;
        Nuclearsource.clip = BuildNuclear;
        Sellsource.clip = UISell;
        Selectsource.clip = UISelect;
        Deniedsource.clip = UIDenied;
        Upgradesource.clip = UIUpgrade;
    }

    public static AudioManager GetAudioManager() {
        if (audioManager == null) {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        return audioManager;
    }

    public void PlayWire() {
        Wiresource.Play();
    }

    public void PlayWind() {
        Windsource.Play();
    }

    public void PlayNuclear() {
        Nuclearsource.Play();
    }

    public void PlaySell() {
        Sellsource.Play();
    }

    public void PlaySelect() {
        Selectsource.Play();
    }

    public void PlayDenied() {
        Deniedsource.Play();
    }

    public void PlayUpgrade() {
        Upgradesource.Play();
    }

	// Update is called once per frame
	void Update () {

        
       
    }
}
