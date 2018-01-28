using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingFiller : MonoBehaviour {

    public GameObject buildingPanelTemplate;
    public GameObject cablePanelTemplate;

	// Use this for initialization
	void Start () {
	    int carriedIndex = 0;
	    for (int i = 0; i < GameManager.GetGameManager().cablesList.Count; i++) {
	        GameObject cable = GameManager.GetGameManager().cablesList[i];
            GameObject cableInfo = Instantiate(cablePanelTemplate);
	        cableInfo.transform.SetParent(transform, false);
	        cableInfo.transform.SetAsFirstSibling();

	        RectTransform rectTransform = cableInfo.GetComponent<RectTransform>();
	        rectTransform.anchoredPosition = new Vector2(rectTransform.localPosition.x, -(i + carriedIndex) * (rectTransform.rect.height + 10) - 10);

	        BuildingInfoObjectStorer objects = cableInfo.GetComponent<BuildingInfoObjectStorer>();

	        objects.nameLabel.text = cable.name;
	        objects.image.sprite = cable.GetComponent<BuildingIcon>().icon;
            objects.costLabel.text = "Cost: " + cable.GetComponent<BuildingCost>().cost + "₡";
            objects.storageLabel.text = "Capacity: " + cable.GetComponent<EnergyTransmitter>().energyCapacity + "PU";

            int index = i + carriedIndex; //Closures ¯\_(ツ)_/¯
	        cableInfo.GetComponent<Button>().onClick.AddListener(delegate { UIManager.GetUIManager().SetSelectedBuilding(index, cableInfo, cable); });
        }

	    carriedIndex = GameManager.GetGameManager().cablesList.Count;

        for (int i = 0; i < GameManager.GetGameManager().producersList.Count; i++) {
            GameObject producer = GameManager.GetGameManager().producersList[i];
            GameObject buildingInfo = Instantiate(buildingPanelTemplate);
            buildingInfo.transform.SetParent(transform, false);
            buildingInfo.transform.SetAsFirstSibling();

	        RectTransform rectTransform = buildingInfo.GetComponent<RectTransform>();
	        rectTransform.anchoredPosition = new Vector2(rectTransform.localPosition.x, -(i + carriedIndex) * (rectTransform.rect.height + 10) - 10);

            BuildingInfoObjectStorer objects = buildingInfo.GetComponent<BuildingInfoObjectStorer>();

            objects.nameLabel.text = producer.name;
            objects.image.sprite = producer.GetComponent<BuildingIcon>().icon;
            objects.costLabel.text = "Cost: " + producer.GetComponent<BuildingCost>().cost + "₡";
            objects.productionLabel.text = "Production: " + producer.GetComponent<EnergyProducer>().energyProduction + "PU/s";
            objects.storageLabel.text = "Capacity: " + producer.GetComponent<EnergyTransmitter>().energyCapacity + "PU";
            objects.upkeepLabel.text = "Upkeep: " + producer.GetComponent<EnergyProducer>().moneyPerSecond + "₡/s";

            int index = i + carriedIndex; //Closures ¯\_(ツ)_/¯
            buildingInfo.GetComponent<Button>().onClick.AddListener(delegate { UIManager.GetUIManager().SetSelectedBuilding(index, buildingInfo, producer);});
        }
	}
    

    // Update is called once per frame
    void Update () {

    }
}
