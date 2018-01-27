﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile : MonoBehaviour {

    public enum Type {Grass, Water};

    private Type type;
    public GameObject building;

    private Color lastColor;
    public Vector2Int gridPosition;

	void Start () {
        type = Type.Grass;
	}

    public void SetType(Type type) {
        switch (type)
        {
            case Type.Water:
                GetComponent<Renderer>().material.color = Color.blue;
                break;
            case Type.Grass:
                break;
        }

        this.type = type;
    }

    public void Select(bool select) {
        if (select)
        {
            lastColor = GetComponent<Renderer>().material.color;
            Color objectColor = GetComponent<Renderer>().material.color;
            GetComponent<Renderer>().material.color = new Color(objectColor.r, objectColor.g, objectColor.b, 0.5f);
        }
        else
        {
            GetComponent<Renderer>().material.color = lastColor;
        }
    }

	public bool CreateBuilding(GameObject buildingPrefab) {
		if (building == null && type == Type.Grass) {
            building = Instantiate(buildingPrefab, transform.position, buildingPrefab.transform.rotation);
		    building.GetComponent<CableManager>().CheckBordering(gridPosition, true);
            return true;
		}
	    return false;
	}
}
