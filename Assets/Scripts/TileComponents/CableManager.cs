using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CableManager : MonoBehaviour {

    private LineRenderer upConnection;
    private LineRenderer downConnection;
    private LineRenderer leftConnection;
    private LineRenderer rightConnection;

    public Vector3 lineOffset;

    private LineRenderer prefab;
    private Vector3 sag = new Vector3(0, -0.1f, 0);

	// Use this for initialization
	void Start () {
	    prefab = null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //-1 = itself
    public void Remove(Vector2Int position, int direction) {
        Tile[] bordering = GameManager.GetGameManager().GetBorderingTiles(position);

        if (direction == -1) {
            for (int i = 0; i < 4; i++) {
                if (bordering[i] == null) {
                    continue;
                }

                Tile tile = bordering[i];

                if (tile.building != null) {
                    GameObject building = tile.building;
                    CableManager cableManager = building.GetComponent<CableManager>();
                    switch (i) {
                        case 0: //Left, sorry for hardcode
                            cableManager.Remove(tile.gridPosition, 2);
                            break;
                        case 1: //Down
                            cableManager.Remove(tile.gridPosition, 3);
                            break;
                        case 2: //Right
                            cableManager.Remove(tile.gridPosition, 0);
                            break;
                        case 3: //Up
                            cableManager.Remove(tile.gridPosition, 1);
                            break;
                    }
                }
            }
        }
        else {
            switch (direction) {
                case 0: //Left, sorry for hardcode
                    if (leftConnection != null) {
                        Destroy(leftConnection);
                    }
                    break;
                case 1: //Down
                    if (downConnection != null) {
                        Destroy(downConnection);
                    }
                    break;
                case 2: //Right
                    if (rightConnection != null) {
                        Destroy(rightConnection);
                    }
                    break;
                case 3: //Up
                    if (upConnection != null) {
                        Destroy(upConnection);
                    }
                    break;
            }
        }
    }

    public void CheckBordering(Vector2Int position, bool recursive) {
        if (prefab == null) {
            prefab = GameManager.GetGameManager().connectionCable;
        }

        Tile[] bordering = GameManager.GetGameManager().GetBorderingTiles(position);
        for(int i = 0; i < 4; i++) {
            if (bordering[i] == null) {
                continue;
            }

            Tile tile = bordering[i];

            if (tile.building != null) {
                GameObject building = tile.building;
                if (recursive) {
                    building.GetComponent<CableManager>().CheckBordering(tile.gridPosition, false);
                }

                if (gameObject.tag == "Consumer" || gameObject.tag == "Producer") {
                    continue;
                }

                LineRenderer line = null;

                switch (i)
                {
                    case 0: //Left, sorry for hardcode
                        if(leftConnection != null)
                            continue;
                        break;
                    case 1: //Down
                        if (downConnection != null)
                            continue;
                        break;
                    case 2: //Right
                        if (rightConnection != null)
                            continue;
                        break;
                    case 3: //Up
                        if (upConnection != null)
                            continue;
                        break;
                }

                line = Instantiate(prefab);
                line.transform.SetParent(gameObject.transform);
                Vector3[] positions = new Vector3[2];
                line.GetPositions(positions);

                if (building.tag == "Cable") { 
                    positions[0] = gameObject.transform.position + lineOffset;

                    Vector3 cableOffset = building.GetComponent<CableManager>().lineOffset;
                    Vector3 middle = (gameObject.transform.position + building.transform.position + lineOffset + cableOffset) / 2 + sag;
                    positions[1] = middle;
                }
                else { 
                    positions[0] = gameObject.transform.position + lineOffset;

                    Vector3 middle = (gameObject.transform.position + building.transform.position) / 2;
                    positions[1] = middle;
                }

                line.SetPositions(positions);

                if (line != null) {
                    switch (i)
                    {
                        case 0: //Left, sorry for hardcode
                            leftConnection = line;
                            break;
                        case 1: //Down
                            downConnection = line;
                            break;
                        case 2: //Right
                            rightConnection = line;
                            break;
                        case 3: //Up
                            upConnection = line;
                            break;
                    }
                }
            }
        }
    }
}
