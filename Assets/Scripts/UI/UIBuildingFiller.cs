using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingFiller : MonoBehaviour {

    public CanvasRenderer buildingPanelTemplate;

	// Use this for initialization
	void Start () {
        for(int i = 0; i < GameManager.GetGameManager().producersList.Count; i++) {
            GameObject producer = GameManager.GetGameManager().producersList[i];
            CanvasRenderer buildingInfo = Instantiate(buildingPanelTemplate);
            buildingInfo.transform.SetParent(transform, false);
            buildingInfo.transform.SetAsFirstSibling();
	        RectTransform rectTransform = buildingInfo.GetComponent<RectTransform>();
	        rectTransform.anchoredPosition = new Vector2(rectTransform.localPosition.x, -i * (rectTransform.rect.height + 10) - 10);

            BuildingInfoObjectStorer objects = buildingInfo.GetComponent<BuildingInfoObjectStorer>();

            objects.nameLabel.text = producer.name;
            objects.costLabel.text = "Cost: ₡₡₡₡";
            objects.productionLabel.text = "Production: " + producer.GetComponent<EnergyProducer>().energyProduction + "PU";
            objects.storageLabel.text = "Energy: " + producer.GetComponent<EnergyTransmitter>().energyCapacity + "PU";
            objects.upkeepLabel.text = "Upkeep: " + producer.GetComponent<EnergyProducer>().moneyPerSecond + "₡";
        }
	}
    

    // Update is called once per frame
    void Update () {

    }
}
