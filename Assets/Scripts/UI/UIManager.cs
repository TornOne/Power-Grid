﻿using System;
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
    private GameObject lastSelectedBuilding;
    private Color lastSelectedBuildingColor;
    public GameObject lastBuilding;

	// Use this for initialization
	void Start () {
	    currentSelection = -1;
	}
	
	// Update is called once per frame
	void Update () {
        if(infoMenu.activeSelf)
	        UpdateInfo();
	}

    public static UIManager GetUIManager() {
        if (uiManager == null) {
            uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        }

        return uiManager;
    }

    public void ShowMenu(bool show, Tile selectedTile) {
        if (!show || selectedTile.GetTileType() == Tile.Type.Water) {
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

    public void SetSelectedBuilding(int index, GameObject selectedBuilding, GameObject building) {
        AudioManager.GetAudioManager().PlayUISelect();

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

    public void UpdateInfo() {
        InfoMenuContainer infoMenuContainer = infoMenu.GetComponent<InfoMenuContainer>();
        Tile selectedTile = GameManager.GetGameManager().GetSelectedTile();
        if (selectedTile != null) {
            GameObject building = selectedTile.building;
            if (building != null) {
                infoMenuContainer.buildingName.text = building.name;

                string infoText = "";

                if (building.tag == "Consumer") {
                    infoText += "Money produced: " + building.GetComponent<EnergyConsumer>().moneyPerSecond + "₡/s\n";
                    infoText += "Energy consumed: " + building.GetComponent<EnergyConsumer>().energyConsumption + "PU/s\n";
                    infoMenuContainer.sellButton.SetActive(false);
                    //infoMenuContainer.upgradeButton.SetActive(false);
                }
                else if (building.tag == "Producer") {
                    infoText += "Upkeep cost: " + building.GetComponent<EnergyProducer>().moneyPerSecond + "₡/s\n";
                    infoText += "Energy produced: " + building.GetComponent<EnergyProducer>().energyProduction + "PU/s\n";
                    infoMenuContainer.sellButton.SetActive(true);
                    //infoMenuContainer.upgradeButton.SetActive(false);
                }
                else if (building.tag == "Cable") {
                    infoText += "Energy consumed: " + building.GetComponent<EnergyConsumer>().energyConsumption + "PU/s\n";
                    infoMenuContainer.sellButton.SetActive(true);
                    //infoMenuContainer.upgradeButton.SetActive(true);
                }

                infoText += "Energy storage: " + Mathf.Round(building.GetComponent<EnergyTransmitter>().currentEnergy) + "/" + building.GetComponent<EnergyTransmitter>().energyCapacity + "PU";

                infoMenuContainer.buildingInfo.text = infoText;
            }
        }
    }
}
