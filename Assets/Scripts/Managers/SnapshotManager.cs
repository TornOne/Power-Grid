using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SnapshotManager : MonoBehaviour {

    public AudioMixerSnapshot Initial;
    public AudioMixerSnapshot StartGame;
    public AudioMixerSnapshot Nuclear;
    public AudioMixerSnapshot EndGame;
    private static SnapshotManager snapshotManager;

	void Start () {

        StartGame.TransitionTo(5);
	}
	public void PauseSnapshot()
    {
        Initial.TransitionTo(1.5f);
            }
    public void ResumeSnapshot()
    {
        StartGame.TransitionTo(0.1f);
    }
    public void FirstNuclear()
    {
        Nuclear.TransitionTo(1);
    }
   public void EndGameSnapshot()
    {
        EndGame.TransitionTo(2);
    }
    public static SnapshotManager GetSnapshotManager()
    {
        if (snapshotManager == null)
        {
            snapshotManager = GameObject.FindGameObjectWithTag("SnapshotManager").GetComponent<SnapshotManager>();
        }

        return snapshotManager;
    }
    void Update () {
		
	}
}
