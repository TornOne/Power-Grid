using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject buyMenu;
    public GameObject infoMenu;

    private static UIManager uiManager;
    public int currentSelection;
    private CanvasRenderer lastSelectedBuilding;
    private Color lastSelectedBuildingColor;
    public GameObject lastBuilding;

	// Use this for initialization
	void Start () {
	    currentSelection = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static UIManager GetUIManager() {
        if (uiManager == null) {
            uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        }

        return uiManager;
    }

    public void ShowMenu(bool show, Tile selectedTile) {
        if (!show) {
            buyMenu.SetActive(false);
            infoMenu.SetActive(false);
            Unselect();
        }
        else {
            if (selectedTile.building != null) {
                infoMenu.SetActive(true);
                buyMenu.SetActive(false);
                Unselect();
            }
            else {
                buyMenu.SetActive(true);
                infoMenu.SetActive(false);
            }
        }
    }

    public void SetSelectedBuilding(int index, CanvasRenderer selectedBuilding, GameObject building) {
        currentSelection = index;
        lastBuilding = building;

        if (lastSelectedBuilding != null)
            lastSelectedBuilding.GetComponent<Image>().color = lastSelectedBuildingColor;

        Image selectedBuildingImage = selectedBuilding.GetComponent<Image>();
        lastSelectedBuildingColor = selectedBuildingImage.color;
        selectedBuildingImage.color = Color.yellow;
        lastSelectedBuilding = selectedBuilding;   
    }

    public void Unselect() {
        currentSelection = -1;

        if (lastSelectedBuilding != null)
            lastSelectedBuilding.GetComponent<Image>().color = lastSelectedBuildingColor;

        lastSelectedBuilding = null;
    }
}
