using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTracker : MonoBehaviour {

    public float totalProduction;
    public float totalConsumption;

    private static PowerTracker powerTracker;

    public static PowerTracker GetPowerTracker() {
        if (powerTracker == null) {
            powerTracker = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PowerTracker>();
        }

        return powerTracker;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
