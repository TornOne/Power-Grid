using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBuilding : MonoBehaviour {
	public List<GameObject> buildings;
	Canvas canvas;

	void Start() {
		canvas = GetComponent<Canvas>();
	}

	public void TileSelected(Tile tile) {
		if (tile.building == null) {
			OpenBuildingMenu(tile);
		} else {
			CloseBuildingMenu();
			OpenBuildingInfo(tile);
		}
	}

	void OpenBuildingMenu(Tile tile) {
		for (int i = 0; i < buildings.Count; i++) {
			//sth sth maybe just pre-build the building UI and show it with this instead of programmatically building it
		}
	}

	void CloseBuildingMenu() {
		
	}

	void OpenBuildingInfo(Tile tile) {
		
	}

	void CloseBuildingInfo() {
		
	}
}
