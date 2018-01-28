using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public AudioClip UISell;
    public AudioClip UISelectTile;
    public AudioClip UIDenied;
    public AudioClip UIUpgrade;
    public AudioClip UISelectMenu;

    private AudioSource audioSource;

    private static AudioManager audioManager;

    void Start () {
        audioSource = GetComponent<AudioSource>();
    }

    public static AudioManager GetAudioManager() {
        if (audioManager == null) {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        return audioManager;
    }

    public void PlaySell() {
        audioSource.PlayOneShot(UISell);
    }

    public void PlaySelect() {
        audioSource.PlayOneShot(UISelectTile);
    }

    public void PlayDenied() {
        audioSource.PlayOneShot(UIDenied);
    }

    public void PlayUpgrade() {
        audioSource.PlayOneShot(UIUpgrade);
    }
    public void PlayUISelect()
    {
        audioSource.PlayOneShot(UISelectMenu);
    }

    public void PlayBuild(AudioClip audioClip) {
        audioSource.PlayOneShot(audioClip);
    }

	// Update is called once per frame
	void Update () {
       
    }
}
