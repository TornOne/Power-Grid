using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public enum Type {Grass, Water};

    public Type type;
    public GameObject building;

    private Color lastColor;

	void Start () {
        type = Type.Grass;
	}

    public void setType(Type type) {
        switch (type)
        {
            case Type.Grass:
                GetComponent<Renderer>().material.color = Color.green;
                break;
            case Type.Water:
                GetComponent<Renderer>().material.color = Color.blue;
                break;
        }

        this.type = type;
    }

    public void Select(bool select) {
        if (select)
        {
            lastColor = GetComponent<Renderer>().material.color;
            Color objectColor = GetComponent<Renderer>().material.color;
            GetComponent<Renderer>().material.color = new Color();
        }
        else
        {
            GetComponent<Renderer>().material.color = lastColor;
        }
    }

	public void CreateBuilding(GameObject buildingPrefab) {
		if (building == null) {
			building = Instantiate(buildingPrefab, this.transform.position, Quaternion.identity);
		}
	}
}
