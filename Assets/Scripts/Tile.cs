using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile : MonoBehaviour {

    public enum Type {Grass, Water};

    public Type type;
    public GameObject building;

    public delegate void ClickAction(GameObject gameObject);
    public static event ClickAction OnClicked;

    private Color lastColor;

    public Vector2Int gridPosition;

	void Start () {
        type = Type.Grass;
	}

    public void setType(Type type) {
        switch (type)
        {
            case Type.Water:
                GetComponent<Renderer>().material.color = Color.blue;
                break;
        }

        this.type = type;
    }

    public void Select(bool select) {
        if (select)
        {
            if (OnClicked != null)
                OnClicked(gameObject);
            lastColor = GetComponent<Renderer>().material.color;
            Color objectColor = GetComponent<Renderer>().material.color;
            GetComponent<Renderer>().material.color = new Color(objectColor.r, objectColor.g, objectColor.b, 0.5f);
        }
        else
        {
            if (OnClicked != null)
                OnClicked(null);
            GetComponent<Renderer>().material.color = lastColor;
        }
    }

	public bool CreateBuilding(GameObject buildingPrefab) {
		if (building == null && type == Type.Grass) {
            building = Instantiate(buildingPrefab, this.transform.position, Quaternion.identity);
		    building.transform.rotation = buildingPrefab.transform.rotation; //Rotation was not carried over from prefab ¯\_(ツ)_/¯
		    building.GetComponent<CableManager>().CheckBordering(gridPosition);
            return true;
		}
	    return false;
	}
}
