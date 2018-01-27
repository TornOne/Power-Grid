using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ConnectCable() {

    }

    public void CheckBordering(Vector2Int position) {
        List<Tile> bordering = GameManager.mainGameManager.GetBorderingTiles(position);
        foreach (var tile in bordering) {
            if (tile.tag == "Cable") {
                Debug.Log("Connect?");
            }
        }
    }
}
